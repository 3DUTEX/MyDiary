using Domain.Entities;

namespace Domain.Repositories;

public interface IUsersRepository
{
    Task<User?> LoadById(int id);
    Task<User?> LoadByEmail(string email);
    Task Create(User user);
}