using GranaFlow.Application.Auth;
using GranaFlow.Application.Dtos;
using GranaFlow.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GranaFlow.API.Controllers
{
    public class HealthController : BaseController
    {
        public HealthController(IHttpContextAccessor httpContextAccessor, InfoToken infoToken) : base(httpContextAccessor, infoToken)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Health()
        {
            return Ok(DateTime.Now);
        }
    }
}
