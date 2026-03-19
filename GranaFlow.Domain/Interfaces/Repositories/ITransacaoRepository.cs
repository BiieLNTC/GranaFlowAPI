using GranaFlow.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Domain.Interfaces.Repositories
{
    public interface ITransacaoRepository
    {
        Task<bool> CreateAsync(Transacao transacao);
        Task<bool> UpdateAsync(Transacao transacao);
        Task<Transacao> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<List<Transacao>> GetAllAsync(DateTime? dataInicial, DateTime? dataFinal);
        Task<List<IGrouping<int, Transacao>>> GetTransacoesPorPessoa();
        Task<List<IGrouping<int, Transacao>>> GetTransacoesPorCategoria();
        Task<List<Transacao>> ObterTransacoesByData(DateTime dataInicio, DateTime dataFim);
        Task<decimal> ObterSaldoTotal();
        Task<List<Transacao>> GetAllDespesas();
        Task<List<Transacao>> GetAllReceitas();
    }
}
