using System.Text.Json;
using Application.Providers;
using Domain.DTOs.Users;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services.Users;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class UsersService : IUsersService
{
    private readonly ILogger<UsersService> _logger;
    private readonly IUsersRepository _usersRepository;
    public UsersService(ILogger<UsersService> logger, IUsersRepository usersRepository)
    {
        _logger = logger;
        _usersRepository = usersRepository;
    }

    public async Task<User> Authenticate(AuthenticateUserDTO authenticateUserDto)
    {
        var user = await _usersRepository.LoadByEmail(authenticateUserDto.Email);

        if (user is null) throw new UnauthorizedException("Invalid credentials!");

        var passwordDecrypted = AESEncryptionHelper.Decrypt(user.Password);

        if (passwordDecrypted != authenticateUserDto.Password) throw new UnauthorizedException("Invalid credentials!");

        return user;
    }

    public async Task<User> Create(CreateUserDTO createUserDTO)
    {
        // Encrypt password before save
        var passwordEncrypted = AESEncryptionHelper.Encrypt(createUserDTO.Password);

        // Update dto in memory
        var user = (User)createUserDTO;
        user.Password = passwordEncrypted;

        // Update user in database
        await _usersRepository.Create(user);

        return user;
    }

    public async Task<User?> LoadById(int id)
        => await _usersRepository.LoadById(id);
}