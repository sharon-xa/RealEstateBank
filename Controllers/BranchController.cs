using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealEstateBank.Data.Dtos.Branch;
using RealEstateBank.Entities;
using RealEstateBank.Helpers;
using RealEstateBank.Services;

namespace RealEstateBank.Controllers;

[Route("api/branches")]
[ApiController]
public class BranchController(IBranchService branchService) : ControllerBase {
    private readonly IBranchService _branchService = branchService;

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpGet]
    public async Task<ActionResult<List<BranchDto>>> GetBranches() {
        return await _branchService.GetBranches();
    }

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpPost]
    public async Task<ActionResult<BranchDto>> AddBranch(BranchForm form) {
        return await _branchService.CreateBranch(form);
    }

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Branch>> DeleteBranch(int id) {
        var deletedBranch = await _branchService.DeleteBranch(id);
        if (deletedBranch == null)
            return NotFound("There's no such branch");
        return NoContent();
    }
}
