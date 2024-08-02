using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Application.Helper;

public static class AuthHelper
{
    public static string GenerateJwtToken(User user)
    {
        var envSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
            ?? throw new ArgumentNullException("<JWT_SECRET> environment is missing!");

        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(envSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([new Claim("User", JsonSerializer.Serialize(user))]),
            Expires = DateTime.UtcNow.AddDays(7),   //Generate token that is valid for 7 days
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}