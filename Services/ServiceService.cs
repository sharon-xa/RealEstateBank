using AutoMapper;

using RealEstateBank.Data;
using RealEstateBank.Data.Dtos.Service;
using RealEstateBank.Entities;
using RealEstateBank.Interface;

namespace RealEstateBank.Services;

public interface IServiceService {
    Task AddService();
    Task<Service> GetService(int id);
    Task<List<ServiceDto>> GetServices();
    Task UpdateService();
    Task DeleteService();
}

public class ServiceService : IServiceService {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ITokenService _tokenService;

    public ServiceService(
        IRepositoryWrapper repositoryWrapper,
        IMapper mapper,
        ITokenService tokenService,
        DataContext context
    ) {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _tokenService = tokenService;
        _context = context;
    }

    public Task AddService() {
        throw new NotImplementedException();
    }

    public Task DeleteService() {
        throw new NotImplementedException();
    }

    public Task<Service> GetService(int id) {
        throw new NotImplementedException();
    }

    public async Task<List<ServiceDto>> GetServices() {
        return await _repositoryWrapper.Services.GetAll<ServiceDto>();
    }

    public Task UpdateService() {
        throw new NotImplementedException();
    }
}
