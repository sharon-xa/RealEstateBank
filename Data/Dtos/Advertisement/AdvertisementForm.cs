using System.ComponentModel.DataAnnotations;

namespace RealEstateBank.Data.Dtos.Advertisement;

public class AdvertisementForm {
    [Required(ErrorMessage = "Title is required")]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters")]
    [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [MinLength(10, ErrorMessage = "Description must be at least 10 characters")]
    public required string Description { get; set; }
}
