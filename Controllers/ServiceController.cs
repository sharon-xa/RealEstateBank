using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealEstateBank.Data.Dtos.Service;
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
    public async Task<ActionResult<ServiceDto>> GetService(int id) {
        var serviceDto = await _serviceService.GetService(id);
        if (serviceDto == null) return NotFound("No such service");
        return serviceDto;
    }

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpPost]
    public async Task<ActionResult<ServiceDto>> AddService([FromBody] ServiceForm form) {
        return await _serviceService.AddService(form);
    }

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpPatch("{id}")]
    public async Task<ActionResult<ServiceDto>> UpdateService([FromRoute] int id, [FromBody] ServiceForm form) {
        var serviceDto = await _serviceService.UpdateService(id, form);
        if (serviceDto == null)
            return NotFound();
        return serviceDto;
    }

    [Authorize(Policy = Policies.RequirePublisherOrAbove)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(int id) {
        await _serviceService.DeleteService(id);
        return NoContent();
    }
}
