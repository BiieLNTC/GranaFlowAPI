using GranaFlow.Application.Auth;
using GranaFlow.Application.Dtos;
using GranaFlow.Application.Services;
using GranaFlow.Application.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GranaFlow.API.Controllers
{
    public class TransacaoController : BaseController
    {
        private readonly TransacaoService _transacaoService;

        public TransacaoController(IHttpContextAccessor httpContextAccessor,
                                TransacaoService service,
                                InfoToken infoToken) : base(httpContextAccessor, infoToken)
        {
            _transacaoService = service;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTransacaoDto transacao)
        {
            var result = await _transacaoService.CreateAsync(transacao);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTransacaoDto transacao)
        {
            var result = await _transacaoService.UpdateAsync(transacao);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _transacaoService.DeleteAsync(id);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _transacaoService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _transacaoService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("ObterTotaisPessoas")]
        public async Task<IActionResult> ObterTotaisPessoas()
        {
            var result = await _transacaoService.ObterTotaisPessoas();
            return Ok(result);
        }

        [HttpGet("ObterTotaisTransacoes")]
        public async Task<IActionResult> ObterTotaisTransacoes()
        {
            var result = await _transacaoService.ObterTotaisTransacoes();
            return Ok(result);
        }
    }
}
