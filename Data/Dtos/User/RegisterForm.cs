using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using RealEstateBank.Data.Enums;

namespace RealEstateBank.Data.Dtos.User;

public class RegisterForm {
    [Required]
    [MinLength(2, ErrorMessage = "FullName must be at least 2 characters")]
    public string? FullName { get; set; }

    [Required]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone Number must be exactly 11 digits")]
    public string? PhoneNumber { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string? Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }

    [Required][EmailAddress] public string? Email { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [Required]
    public Gender? Gender { get; set; }

    [Required][DataType(DataType.Date)] public DateOnly? Birthday { get; set; }
}
