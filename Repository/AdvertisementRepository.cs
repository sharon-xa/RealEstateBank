using AutoMapper;

using RealEstateBank.Data;
using RealEstateBank.Entities;
using RealEstateBank.Interface;

namespace RealEstateBank.Repository;

public class AdvertisementRepository : GenericRepository<Advertisement, int>, IAdvertisementRepository {
    public AdvertisementRepository(DataContext context, IMapper mapper) : base(context, mapper) {
    }

}
