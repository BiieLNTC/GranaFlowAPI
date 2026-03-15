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
        Task<bool> UpdateAsync(Categoria categoria);
        Task<Categoria> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<List<Categoria>> GetAllAsync();
    }
}