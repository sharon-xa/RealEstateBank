using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RealEstateBank.Data.Dtos.Branch;

public class BranchForm {
    [Required]
    [StringLength(100, ErrorMessage = "City name can't be longer than 100 characters")]
    public required string City { get; set; }

    [Required]
    [Phone(ErrorMessage = "Invalid phone number format")]
    [StringLength(20, ErrorMessage = "Phone number can't be longer than 20 characters")]
    public required string PhoneNumber { get; set; }

    [Required]
    [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters")]
    public required string Description { get; set; }

    [Required]
    [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
    public required decimal Lat { get; set; }

    [Required]
    [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
    [JsonPropertyName("Long")]
    public required decimal Long { get; set; }
}
