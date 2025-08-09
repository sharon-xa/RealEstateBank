using AutoMapper;

using RealEstateBank.Data;
using RealEstateBank.Interface;

namespace RealEstateBank.Repository;

public class RepositoryWrapper : IRepositoryWrapper {
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    public RepositoryWrapper(DataContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
        Users ??= new UserRepository(_context, _mapper);
    }

    public IUserRepository Users { get; }
}
