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
            var result = await _db.Categorias.AnyAsync(a => a.Descricao == descricao && a.Id != id);

            return result;
        }

        public async Task<bool> CreateAsync(Categoria categoria)
        {
            _db.Categorias.Add(categoria);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateAsync(UpdateCategoriaDto updateCategoria) // mudar para categoria
        {
            var result = await _db.Categorias.Where(w => w.UsuarioId == _infoToken.Id && w.Id == updateCategoria.Id)
                .ExecuteUpdateAsync(ex =>
                {
                    ex.SetProperty(s => s.Descricao, updateCategoria.Descricao);
                    ex.SetProperty(s => s.Finalidade, updateCategoria.Finalidade);
                    ex.SetProperty(s => s.Cor, updateCategoria.Cor);
                });

            return result > 0;
        }
    }
}
