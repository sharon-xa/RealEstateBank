using System.ComponentModel.DataAnnotations;

namespace RealEstateBank.Data.Dtos.User;

public class UpdateUserForm
{
    [EmailAddress]
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime BirthDay { get; set; }
    public int? Role { get; set; }
    public string? Img { get; set; }
}
