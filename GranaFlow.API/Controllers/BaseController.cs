using GranaFlow.Application.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GranaFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        public readonly InfoToken _infoToken;

        public BaseController(IHttpContextAccessor httpContextAccessor, InfoToken infoToken)
        {
            _infoToken = infoToken;
        }
    }
}
