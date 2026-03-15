using GranaFlow.Application.Auth;
using GranaFlow.Application.Dtos;
using GranaFlow.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GranaFlow.API.Controllers
{
    public class PessoaController : BaseController
    {
        private readonly PessoaService _pessoaService;

        public PessoaController(IHttpContextAccessor httpContextAccessor, InfoToken infoToken, PessoaService service) : base(httpContextAccessor, infoToken)
        {
            _pessoaService = service;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePessoaDto categoria)
        {
            var result = await _pessoaService.CreateAsync(categoria);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdatePessoaDto categoria)
        {
            var result = await _pessoaService.UpdateAsync(categoria);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _pessoaService.DeleteAsync(id);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _pessoaService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _pessoaService.GetAllAsync();
            return Ok(result);
        }
    }
}
