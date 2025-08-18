using AutoMapper;

using RealEstateBank.Data;
using RealEstateBank.Entities;
using RealEstateBank.Interface;

namespace RealEstateBank.Repository;

public class ServiceRepository : GenericRepository<Service, int>, IServiceRepository {
    public ServiceRepository(
        DataContext context,
        IMapper mapper
    ) : base(context, mapper) {
    }
}
