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
        Task<List<Transacao>> GetByFiltroAsync(DateTime? dataInicio, DateTime? dataFim, IEnumerable<int> listPessoasId, IEnumerable<int> listCategoriasId);
    }                                           
}
