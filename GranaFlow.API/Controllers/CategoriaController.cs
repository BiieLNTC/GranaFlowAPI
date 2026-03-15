using GranaFlow.Application.Auth;
using GranaFlow.Application.Dtos;
using GranaFlow.Application.Services;
using GranaFlow.Application.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GranaFlow.API.Controllers
{
    public class CategoriaController : BaseController
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaController(IHttpContextAccessor httpContextAccessor, InfoToken infoToken, CategoriaService service) : base(httpContextAccessor, infoToken)
        {
            _categoriaService = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoriaDto categoria)
        {
            var result = await _categoriaService.CreateAsync(categoria);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCategoriaDto categoria)
        {
            var result = await _categoriaService.UpdateAsync(categoria);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _categoriaService.DeleteAsync(id);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _categoriaService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _categoriaService.GetAllAsync();
            return Ok(result);
        }
    }
}
