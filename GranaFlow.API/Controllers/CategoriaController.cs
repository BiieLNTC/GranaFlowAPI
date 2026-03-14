using GranaFlow.Application.Auth;
using GranaFlow.Application.Dtos;
using GranaFlow.Application.Services;
using GranaFlow.Application.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GranaFlow.API.Controllers
{
    public class CategoriaController : BaseController
    {
        public readonly InfoToken _infoToken;
        private readonly CategoriaService _categoriaService;

        public CategoriaController(IHttpContextAccessor httpContextAccessor, InfoToken infoToken, CategoriaService service) : base(httpContextAccessor, infoToken)
        {
            _categoriaService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoriaDto categoria)
        {
            var result = await _categoriaService.CreateAsync(categoria);

            return Ok(result);
        }
    }
}
