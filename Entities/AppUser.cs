using System.ComponentModel.DataAnnotations;

using RealEstateBank.Data.Enums;

namespace RealEstateBank.Entities;

public class AppUser : BaseEntity<Guid> {
    [Required][MaxLength(100)] public string FullName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Required] public string PasswordHash { get; set; } = null!;

    [Required]
    [RegularExpression(@"^\d{11}$")]
    public string PhoneNumber { get; set; } = null!;

    public string? RefreshToken { get; set; }

    [Required] public UserRole Role { get; set; }

    [Required] public Gender Gender { get; set; }

    [Required] public DateOnly Birthday { get; set; }
}
