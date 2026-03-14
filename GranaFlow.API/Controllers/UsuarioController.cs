using GranaFlow.API.Constants;
using GranaFlow.Application.Auth;
using GranaFlow.Application.Dtos;
using GranaFlow.Application.Services;
using GranaFlow.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace GranaFlow.API.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly UsuarioService _service;
        public readonly CryptoUtils _cryptoUtils;

        public UsuarioController(IHttpContextAccessor httpContextAccessor, 
                                UsuarioService service,
                                CryptoUtils cryptoUtils,
                                InfoToken infoToken) : base(httpContextAccessor, infoToken)
        {
            _service = service;
            _cryptoUtils = cryptoUtils;
        }

        [HttpPost(UsuarioApiConstants.CreateAccount)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount([FromBody] CreateUserDto createUser)
        {
            var obj = await _service.CreateAccount(createUser);
            return Ok(obj);
        }

        [HttpPost(UsuarioApiConstants.Login)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var obj = await _service.Login(login);
            return Ok(obj);
        }
    }
}
