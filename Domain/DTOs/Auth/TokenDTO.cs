using Domain.Entities;

namespace Domain.DTOs.Auth;

public class TokenDTO
{
    public required User User { get; set; }
}