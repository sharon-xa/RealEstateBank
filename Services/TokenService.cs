using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

using RealEstateBank.Data.Dtos.User;

namespace RealEstateBank.Services;

public interface ITokenService {
    string CreateToken(UserDto user);
    string CreateRefreshToken(UserDto user);
}

public class TokenService : ITokenService {
    private readonly int _accessExp;
    private readonly string _audience;
    private readonly string _issuer;
    private readonly SymmetricSecurityKey _key;
    private readonly int _refreshExp;

    public TokenService(IConfiguration config) {
        var tokenKeyString = config["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(tokenKeyString))
            throw new Exception("No JWT key found");

        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKeyString));

        var issuer = config["Jwt:Issuer"];
        if (issuer == null)
            throw new Exception("No issuer found, Token service constructor");

        var audience = config["Jwt:Audience"];
        if (audience == null)
            throw new Exception("No audience found, Token service constructor");

        var accessExpStr = config["Jwt:AccessTokenExpInMin"];
        if (accessExpStr == null)
            throw new Exception("No access token expiry found");

        var refreshExpStr = config["Jwt:RefreshTokenExpInDays"];
        if (refreshExpStr == null)
            throw new Exception("No access token expiry found");

        var parsed = int.TryParse(accessExpStr, out var accessExp);
        if (!parsed)
            throw new Exception("Failed to parse 'Jwt:AccessTokenExpInMin' into an integer.");

        parsed = int.TryParse(refreshExpStr, out var refreshExp);
        if (!parsed)
            throw new Exception("Failed to parse 'Jwt:RefreshTokenExpInDays' into an integer.");

        _issuer = issuer;
        _audience = audience;
        _accessExp = accessExp;
        _refreshExp = refreshExp;
    }

    public string CreateToken(UserDto user) {
        var claims = GenerateClaims(user);
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(_accessExp),
            SigningCredentials = creds,
            Issuer = _issuer,
            Audience = _audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string CreateRefreshToken(UserDto user) {
        var claims = GenerateClaims(user);
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(_refreshExp),
            SigningCredentials = creds,
            Issuer = _issuer,
            Audience = _audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static List<Claim> GenerateClaims(UserDto user) {
        return [
            new Claim("sub", user.Id.ToString(), ClaimValueTypes.String),
            new Claim(JwtRegisteredClaimNames.Name, user.FullName, ClaimValueTypes.String),
            new Claim("role", user.Role.ToString(), ClaimValueTypes.String)
        ];
    }
}
