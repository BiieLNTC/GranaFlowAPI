using GranaFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Domain.Interfaces.Repositories
{
    public interface ICategoriaRepository
    {
        Task<bool> ExistsAsync(string descricao, int id = 0);
        Task<bool> CreateAsync(Categoria categoria);
        Task<bool> UpdateAsync(UpdateCategoriaDto updateCategoria);
    }
}