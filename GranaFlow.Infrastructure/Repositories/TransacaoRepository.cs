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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<List<Transacao>> GetAllAsync(DateTime? dataInicial, DateTime? dataFinal)
        {
            var query = _db.Transacoes
                .AsNoTracking()
                .Include(i => i.Categoria)
                .Include(i => i.Pessoa)
                .Where(w => w.UsuarioId == _infoToken.Id)
                .AsSplitQuery();

            if (dataInicial.HasValue)
            {
                query = query.Where(w => w.DataTransacao >= dataInicial.Value);
            }

            if (dataFinal.HasValue)
            {
                var dataFim = dataFinal.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(w => w.DataTransacao <= dataFim);
            }

            return await query.ToListAsync();
        }

        public async Task<List<IGrouping<int, Transacao>>> GetTransacoesPorPessoa(DateTime? dataInicial, DateTime? dataFinal)
        {
            var groupTransacoes = _db.Transacoes.AsNoTracking()
                            .Include(i => i.Pessoa)
                            .Where(w => w.UsuarioId == _infoToken.Id)
                            .AsSplitQuery();


            if (dataInicial.HasValue)
            {
                groupTransacoes = groupTransacoes.Where(w => w.DataTransacao >= dataInicial.Value);
            }

            if (dataFinal.HasValue)
            {
                var dataFim = dataFinal.Value.Date.AddDays(1).AddTicks(-1);
                groupTransacoes = groupTransacoes.Where(w => w.DataTransacao <= dataFim);
            }

            return await groupTransacoes.GroupBy(g => g.PessoaId).ToListAsync();
        }

        public async Task<List<IGrouping<int, Transacao>>> GetTransacoesPorCategoria(DateTime? dataInicial, DateTime? dataFinal)
        {
            var groupTransacoes = _db.Transacoes.AsNoTracking()
                            .Include(i => i.Categoria)
                            .Where(w => w.UsuarioId == _infoToken.Id)
                            .AsSplitQuery();

            if (dataInicial.HasValue)
            {
                groupTransacoes = groupTransacoes.Where(w => w.DataTransacao >= dataInicial.Value);
            }

            if (dataFinal.HasValue)
            {
                var dataFim = dataFinal.Value.Date.AddDays(1).AddTicks(-1);
                groupTransacoes = groupTransacoes.Where(w => w.DataTransacao <= dataFim);
            }

            return await groupTransacoes.GroupBy(g => g.CategoriaId).ToListAsync();
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

        public async Task<List<Transacao>> GetAllDespesas(DateTime? dataInicial, DateTime? dataFinal)
        {
            var query = _db.Transacoes.AsNoTracking()
                                        .Include(i => i.Categoria)
                                        .Where(w => w.UsuarioId == _infoToken.Id && w.Tipo == ETipoTransacao.Despesa)
                                        .AsSplitQuery();


            if (dataInicial.HasValue)
            {
                query = query.Where(w => w.DataTransacao >= dataInicial.Value);
            }

            if (dataFinal.HasValue)
            {
                var dataFim = dataFinal.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(w => w.DataTransacao <= dataFim);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Transacao>> GetAllReceitas(DateTime? dataInicial, DateTime? dataFinal)
        {
            var query = _db.Transacoes.AsNoTracking()
                                        .Include(i => i.Categoria)
                                        .Where(w => w.UsuarioId == _infoToken.Id && w.Tipo == ETipoTransacao.Receita)
                                        .AsSplitQuery();

            if (dataInicial.HasValue)
            {
                query = query.Where(w => w.DataTransacao >= dataInicial.Value);
            }

            if (dataFinal.HasValue)
            {
                var dataFim = dataFinal.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(w => w.DataTransacao <= dataFim);
            }

            return await query.ToListAsync();
        }
    }
}
