using AutoMapper;

using RealEstateBank.Data;
using RealEstateBank.Entities;
using RealEstateBank.Interface;

namespace RealEstateBank.Repository;

public class AcceptedCitizenRepository : GenericRepository<AcceptedCitizen, int>, IAcceptedCitizenRepository {
    public AcceptedCitizenRepository(DataContext context, IMapper mapper) : base(context, mapper) {
    }
}
