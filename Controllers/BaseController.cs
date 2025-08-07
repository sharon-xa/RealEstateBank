using Microsoft.AspNetCore.Mvc;

namespace RealEstateBank.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected string MethodType => HttpContext.Request.Method;
}