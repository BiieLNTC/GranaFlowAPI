using GranaFlow.Application.Auth;
using GranaFlow.Domain.Entities;
using GranaFlow.Domain.Interfaces.Repositories;
using GranaFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Infrastructure.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly GranaFlowContext _db;
        public readonly InfoToken _infoToken;

        public PessoaRepository(GranaFlowContext db, InfoToken infoToken)
        {
            _db = db;
            _infoToken = infoToken;
        }

        public async Task<bool> ExistsAsync(string nome, int id = 0)
        {
            var result = await _db.Pessoas.AnyAsync(a => a.Nome == nome && a.Id != id);

            return result;
        }

        public async Task<bool> CreateAsync(Pessoa categoria)
        {
            _db.Pessoas.Add(categoria);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateAsync(Pessoa categoria) 
        {
            _db.Pessoas.Update(categoria);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _db.Pessoas.Where(w => w.UsuarioId == _infoToken.Id && w.Id == id)
                                      .ExecuteDeleteAsync();

            return result > 0;
        }

        public async Task<Pessoa> GetByIdAsync(int id)
        {
            return await _db.Pessoas.AsNoTracking()
                                       .Where(w => w.UsuarioId == _infoToken.Id && w.Id == id)
                                       .FirstOrDefaultAsync();
        }

        public async Task<List<Pessoa>> GetAllAsync()
        {
            return await _db.Pessoas.AsNoTracking()
                                 .Where(w => w.UsuarioId == _infoToken.Id)
                                 .ToListAsync();
        }
    }
}
