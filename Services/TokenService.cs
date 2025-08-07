using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RealEstateBank.Data.Dtos.User;
using JwtClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace RealEstateBank.Services;

public interface ITokenService
{
    string CreateToken(UserDto user);
    string CreateRefreshToken(UserDto user);
}

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
        var tokenKeyString = config["TokenKey"];

        if (String.IsNullOrWhiteSpace(tokenKeyString))
            throw new Exception("No Token Key Found");

        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKeyString));
    }

    public string CreateToken(UserDto user)
    {
        var claims = new List<Claim>
        {
            new(JwtClaimNames.NameId, user.Id.ToString()),
            new("id", user.Id.ToString()),
            new("Role", user.Role.ToString()),
            new("ExpiryDate", DateTime.UtcNow.AddMinutes(15).ToString()),
            // new Claim(JwtRegisteredClaimNames., user.Email.ToString()),
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


    public string CreateRefreshToken(UserDto user)
    {
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var claims = new List<Claim>
        {
            new(JwtClaimNames.NameId, user.Id.ToString()),
            new("id", user.Id.ToString()),
            new("ExpiryDate", DateTime.UtcNow.AddMinutes(15).ToString()),
            // new Claim(JwtRegisteredClaimNames., user.Email.ToString()),
        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}