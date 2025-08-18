using AutoMapper;

using RealEstateBank.Data;
using RealEstateBank.Data.Dtos.Advertisement;
using RealEstateBank.Entities;
using RealEstateBank.Interface;
using RealEstateBank.Utils;
using RealEstateBank.Utils.Exceptions;

namespace RealEstateBank.Services;

public interface IAdvertisementService {
    Task<PaginatedResult<AdvertisementDto>> GetAll(PagingParams pagingParams);
    Task<AdvertisementDto> Create(AdvertisementForm form);
    Task Delete(int id);
}

public class AdvertisementService : IAdvertisementService {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public AdvertisementService(
        IRepositoryWrapper repositoryWrapper,
        IMapper mapper,
        DataContext context
    ) {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _context = context;
    }

    public async Task<AdvertisementDto> Create(AdvertisementForm form) {
        var ad = _mapper.Map<Advertisement>(form);
        var adModel = await _repositoryWrapper.Advertisements.Add(ad);
        return _mapper.Map<AdvertisementDto>(adModel);
    }

    public async Task Delete(int id) {
        var adModel = await _repositoryWrapper.Advertisements.Delete(id);
        if (adModel == null)
            throw new AppException(
                "Advertisement not found",
                nameof(AdvertisementService),
                nameof(Delete),
                StatusCodes.Status404NotFound
            );
    }

    public async Task<PaginatedResult<AdvertisementDto>> GetAll(PagingParams pagingParams) {
        return await _repositoryWrapper.Advertisements.GetAll<AdvertisementDto>(pagingParams);
    }
}
