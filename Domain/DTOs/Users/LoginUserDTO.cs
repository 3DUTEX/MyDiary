using Domain.Entities;

namespace Domain.DTOs.Users;

public class AuthenticateUserDTO
{
    public required string Email { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
}