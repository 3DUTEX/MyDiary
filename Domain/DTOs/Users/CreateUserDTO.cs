using Domain.Entities;

namespace Domain.DTOs.Users;

public class CreateUserDTO
{
    public string Nickname { get; set; } = string.Empty;
    public required string Email { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;

    public static explicit operator User(CreateUserDTO dto)
    {
        var user = new User()
        {
            Email = dto.Email,
            Password = dto.Password,
            Nickname = dto.Nickname
        };
        return user;
    }
}