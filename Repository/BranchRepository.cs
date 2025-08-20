using AutoMapper;

using RealEstateBank.Data;
using RealEstateBank.Entities;
using RealEstateBank.Interface;

namespace RealEstateBank.Repository;

public class BranchRepository : GenericRepository<Branch, int>, IBranchRepository {
    public BranchRepository(DataContext context, IMapper mapper) : base(context, mapper) {
    }


}
