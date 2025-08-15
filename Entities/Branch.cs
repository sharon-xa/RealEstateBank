namespace RealEstateBank.Entities;

public class Branch : BaseEntity<int> {
    public required string City { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Description { get; set; }
    public required decimal Lat { get; set; }
    public required decimal Long { get; set; }

    // Navigation property
    public ICollection<AcceptedCitizen> AcceptedCitizens { get; set; } = new List<AcceptedCitizen>();
}
