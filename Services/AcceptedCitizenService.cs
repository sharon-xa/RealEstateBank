using AutoMapper;

using Microsoft.EntityFrameworkCore;

using RealEstateBank.Data;
using RealEstateBank.Data.Dtos.AcceptedCitizen;
using RealEstateBank.Interface;
using RealEstateBank.Utils;

namespace RealEstateBank.Services;

public interface IAcceptedCitizenService {
    Task<PaginatedResult<AcceptedCitizenDto>> GetAll(PagingParams pagingParams);
    Task AddAll(IFormFile file);
}

public class AcceptedCitizenService : IAcceptedCitizenService {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public AcceptedCitizenService(
        IRepositoryWrapper repositoryWrapper,
        IMapper mapper,
        DataContext context
    ) {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _context = context;
    }

    public Task AddAll(IFormFile file) {
        throw new NotImplementedException();
    }

    public async Task<PaginatedResult<AcceptedCitizenDto>> GetAll(PagingParams pagingParams) {
        return await _repositoryWrapper.AcceptedCitizens.GetAll<AcceptedCitizenDto>(
            query => query.Include(c => c.Branch),
            pagingParams
        );
    }
}
