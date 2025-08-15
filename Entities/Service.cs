using System.ComponentModel.DataAnnotations;

namespace RealEstateBank.Entities;

public class Service : BaseEntity<int> {
    [Required] public required string Title { get; set; }
    [Required] public required string Content { get; set; }
}
