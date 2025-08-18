using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealEstateBank.Data.Dtos.Service;
using RealEstateBank.Entities;
using RealEstateBank.Helpers;
using RealEstateBank.Services;

namespace RealEstateBank.Controllers;

[Route("api/services")]
[ApiController]
public class ServiceController(IServiceService service) : BaseController {
    private readonly IServiceService _serviceService = service;

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpGet]
    public async Task<ActionResult<List<ServiceDto>>> GetServices() {
        return await _serviceService.GetServices();
    }

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpGet("{id}")]
    public Task<IActionResult> GetService(int id) {
        throw new NotImplementedException();
    }

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpPost]
    public Task<IActionResult> AddService() {
        throw new NotImplementedException();
    }

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpPatch("{id}")]
    public Task<IActionResult> UpdateService(int id) {
        throw new NotImplementedException();
    }

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpDelete("{id}")]
    public Task<IActionResult> DeleteService(int id) {
        throw new NotImplementedException();
    }
}
