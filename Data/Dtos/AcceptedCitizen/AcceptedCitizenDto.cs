using RealEstateBank.Data.Dtos.Branch;
using RealEstateBank.Entities;

namespace RealEstateBank.Data.Dtos.AcceptedCitizen;

public class AcceptedCitizenDto {
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string FullName { get; set; }
    public bool AcceptanceState { get; set; }
    public BranchDto? Branch { get; set; }
}
