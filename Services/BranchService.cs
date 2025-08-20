using AutoMapper;

using RealEstateBank.Data;
using RealEstateBank.Data.Dtos.Branch;
using RealEstateBank.Entities;
using RealEstateBank.Interface;

namespace RealEstateBank.Services;

public interface IBranchService {
    Task<List<BranchDto>> GetBranches();
    Task<BranchDto> CreateBranch(BranchForm form);
    Task<Branch?> DeleteBranch(int id);
}

public class BranchService : IBranchService {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public BranchService(
        IRepositoryWrapper repositoryWrapper,
        IMapper mapper,
        DataContext context
    ) {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<BranchDto>> GetBranches() {
        return await _repositoryWrapper.Branches.GetAll<BranchDto>();
    }

    public async Task<BranchDto> CreateBranch(BranchForm form) {
        var branchModel = _mapper.Map<Branch>(form);
        var branch = await _repositoryWrapper.Branches.Add(branchModel);
        return _mapper.Map<BranchDto>(branch);
    }

    public async Task<Branch?> DeleteBranch(int id) {
        return await _repositoryWrapper.Branches.Delete(id);
    }
}
