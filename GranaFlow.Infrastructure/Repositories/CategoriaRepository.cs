using GranaFlow.Application.Auth;
using GranaFlow.Application.Dtos;
using GranaFlow.Domain.Entities;
using GranaFlow.Domain.Interfaces.Repositories;
using GranaFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly GranaFlowContext _db;
        public readonly InfoToken _infoToken;

        public CategoriaRepository(GranaFlowContext db, InfoToken infoToken)
        {
            _db = db;
            _infoToken = infoToken;
        }

        public async Task<bool> ExistsAsync(string descricao, int id = 0)
        {
            var result = await _db.Categorias.AnyAsync(a => a.UsuarioId == _infoToken.Id && a.Descricao == descricao && a.Id != id);

            return result;
        }

        public async Task<bool> CreateAsync(Categoria categoria)
        {
            _db.Categorias.Add(categoria);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateAsync(Categoria categoria)
        {
            _db.Categorias.Update(categoria);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _db.Categorias.Where(w => w.UsuarioId == _infoToken.Id && w.Id == id)
                                      .ExecuteDeleteAsync();

            return result > 0;
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            return await _db.Categorias.AsNoTracking()
                                       .Where(w => w.UsuarioId == _infoToken.Id && w.Id == id)
                                       .FirstOrDefaultAsync();
        }

        public async Task<List<Categoria>> GetAllAsync()
        {
            return await _db.Categorias.AsNoTracking()
                                 .Where(w => w.UsuarioId == _infoToken.Id)
                                 .ToListAsync();
        }
    }
}
