using GranaFlow.Application.Auth;
using GranaFlow.Application.Dtos;
using GranaFlow.Domain.Entities;
using GranaFlow.Domain.Enums;
using GranaFlow.Domain.Interfaces.Repositories;
using GranaFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToolSharp.Utils;

namespace GranaFlow.Infrastructure.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly GranaFlowContext _db;
        public readonly InfoToken _infoToken;

        public TransacaoRepository(GranaFlowContext db, InfoToken infoToken)
        {
            _db = db;
            _infoToken = infoToken;
        }

        public async Task<bool> CreateAsync(Transacao transacao)
        {
            _db.Transacoes.Add(transacao);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateAsync(Transacao transacao)
        {
            _db.Transacoes.Update(transacao);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Transacao> GetByIdAsync(int id)
        {
            return await _db.Transacoes.AsNoTracking()
                           .Where(w => w.UsuarioId == _infoToken.Id && w.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _db.Transacoes.Where(w => w.UsuarioId == _infoToken.Id && w.Id == id)
                          .ExecuteDeleteAsync();

            return result > 0;
        }

        public async Task<List<Transacao>> GetAllAsync()
        {
            return await _db.Transacoes.AsNoTracking()
                                        .Include(i => i.Categoria)
                                        .Include(i => i.Pessoa)
                                        .Where(w => w.UsuarioId == _infoToken.Id)
                                        .AsSplitQuery()
                                        .ToListAsync();
        }

        public async Task<List<IGrouping<int, Transacao>>> GetTransacoesPorPessoa()
        {
            var groupTransacoes = await _db.Transacoes.AsNoTracking()
                            .Include(i => i.Pessoa)
                            .Where(w => w.UsuarioId == _infoToken.Id)
                            .AsSplitQuery()
                            .GroupBy(g => g.PessoaId)
                            .ToListAsync();

            return groupTransacoes;
        }

        public async Task<List<Transacao>> ObterTransacoesByData(DateTime dataInicio, DateTime dataFim)
        {
            return await _db.Transacoes.AsNoTracking()
                                        .Where(w => w.UsuarioId == _infoToken.Id
                                                && w.DataTransacao >= dataInicio
                                                && w.DataTransacao <= dataFim)
                                        .ToListAsync();
        }

        public async Task<decimal> ObterSaldoTotal()
        {
            return await _db.Transacoes
                                .AsNoTracking()
                                .Where(t => t.UsuarioId == _infoToken.Id)
                                .SumAsync(t => t.Tipo == ETipoTransacao.Receita ? t.Valor : -t.Valor);
        }
    }
}
