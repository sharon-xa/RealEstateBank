namespace RealEstateBank.Data.Dtos.Branch;

public class BranchDto {
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string City { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Description { get; set; }
    public required decimal Lat { get; set; }
    public required decimal Long { get; set; }
}
