using AutoMapper;

using RealEstateBank.Data;
using RealEstateBank.Interface;

namespace RealEstateBank.Repository;

public class RepositoryWrapper : IRepositoryWrapper {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public IUserRepository Users { get; }
    public IBankRepository Bank { get; }
    public IAdvertisementRepository Advertisements { get; }
    public IServiceRepository Services { get; }
    public IAcceptedCitizenRepository AcceptedCitizens { get; }
    public IBranchRepository Branches { get; }

    public RepositoryWrapper(DataContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
        Users ??= new UserRepository(_context, _mapper);
        Bank ??= new BankRepository(_context, _mapper);
        Advertisements ??= new AdvertisementRepository(_context, _mapper);
        Services ??= new ServiceRepository(_context, _mapper);
        AcceptedCitizens ??= new AcceptedCitizenRepository(_context, _mapper);
        Branches ??= new BranchRepository(_context, _mapper);
    }
}
