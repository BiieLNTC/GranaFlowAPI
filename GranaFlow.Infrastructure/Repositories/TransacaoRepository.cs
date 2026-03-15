using GranaFlow.Application.Auth;
using GranaFlow.Application.Dtos;
using GranaFlow.Domain.Entities;
using GranaFlow.Domain.Interfaces.Repositories;
using GranaFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToolSharp.Utils;

namespace GranaFlow.Infrastructure.Repositories
{
    internal class TransacaoRepository : ITransacaoRepository
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

        public async Task<List<Transacao>> GetByFiltroAsync(FiltroTransacaoDto filtro)
        {
            var query = _db.Transacoes.Where(w => w.UsuarioId == _infoToken.Id).AsQueryable();

            if (filtro.DataInicio.HasValue)
            {
                query = query.Where(t => t.DataTransacao >= filtro.DataInicio.Value);
            }

            if (filtro.DataFim.HasValue)
            {
                query = query.Where(t => t.DataTransacao <= filtro.DataFim.Value);
            }

            if (filtro.ListUsuariosId.IsNotNullOrEmpty())
            {
                query = query.Where(t => filtro.ListUsuariosId.Contains(t.UsuarioId));
            }

            if (filtro.ListCategoriasId != null && filtro.ListCategoriasId.Any())
            {
                query = query.Where(t => filtro.ListCategoriasId.Contains(t.CategoriaId));
            }

            return await query.ToListAsync();
        }
    }
}
