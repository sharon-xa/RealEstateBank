using RealEstateBank.Data.Enums;

namespace RealEstateBank.Data.Dtos.User;

public class UserDto {
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public UserRole Role { get; set; }
    public Gender Gender { get; set; }
    public DateOnly Birthday { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}
