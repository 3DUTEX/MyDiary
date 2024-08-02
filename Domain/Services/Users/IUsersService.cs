using Domain.DTOs.Users;
using Domain.Entities;

namespace Domain.Services.Users;

public interface IUsersService
{
    Task<User> Authenticate(AuthenticateUserDTO authenticateUserDto);
    Task<User> Create(CreateUserDTO createUserDTO);
    Task<User?> LoadById(int id);
}