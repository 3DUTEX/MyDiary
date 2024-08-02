using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    AppDbContext _context;

    public UsersRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task Create(User user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    public async Task<User?> LoadByEmail(string email)
        => await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

    public async Task<User?> LoadById(int id)
        => await _context.Users.FindAsync(id);
}