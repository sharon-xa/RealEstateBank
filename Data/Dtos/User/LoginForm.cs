using System.ComponentModel.DataAnnotations;

namespace RealEstateBank.Data.Dtos.User;

public class LoginForm
{
    [EmailAddress]
    public string? Email { get; set; }
    public string? Password { get; set; }
}
