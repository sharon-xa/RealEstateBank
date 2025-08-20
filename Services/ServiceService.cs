using AutoMapper;

using Microsoft.EntityFrameworkCore;

using RealEstateBank.Data;
using RealEstateBank.Data.Dtos.Service;
using RealEstateBank.Entities;
using RealEstateBank.Interface;

namespace RealEstateBank.Services;

public interface IServiceService {
    Task<ServiceDto> AddService(ServiceForm form);
    Task<ServiceDto?> GetService(int id);
    Task<List<ServiceDto>> GetServices();
    Task<ServiceDto?> UpdateService(int id, ServiceForm form);
    Task<Service?> DeleteService(int id);
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

    public async Task<ServiceDto> AddService(ServiceForm form) {
        var serviceModel = _mapper.Map<Service>(form);
        var service = await _repositoryWrapper.Services.Add(serviceModel);
        return _mapper.Map<ServiceDto>(service);
    }

    public async Task<ServiceDto?> GetService(int id) {
        var serviceModel = await _repositoryWrapper.Services.GetById(id);
        if (serviceModel == null)
            return null;

        return _mapper.Map<ServiceDto>(serviceModel);
    }

    public async Task<List<ServiceDto>> GetServices() {
        return await _repositoryWrapper.Services.GetAll<ServiceDto>();
    }

    public async Task<ServiceDto?> UpdateService(int id, ServiceForm form) {
        var service = _mapper.Map<Service>(form);
        service.Id = id;
        var serviceModel = await _repositoryWrapper.Services.Update(service);
        if (serviceModel == null)
            return null;
        return _mapper.Map<ServiceDto>(serviceModel);
    }

    public async Task<Service?> DeleteService(int id) {
        var serviceModel = await _repositoryWrapper.Services.Delete(id);
        if (serviceModel == null) return null;
        return serviceModel;
    }
}
