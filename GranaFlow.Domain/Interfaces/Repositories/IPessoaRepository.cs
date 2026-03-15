using GranaFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Domain.Interfaces.Repositories
{
    public interface IPessoaRepository
    {
        Task<bool> ExistsAsync(string nome, int id = 0);
        Task<bool> CreateAsync(Pessoa pessoa);
        Task<bool> UpdateAsync(Pessoa pessoa);
        Task<Pessoa> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<List<Pessoa>> GetAllAsync();
    }
}
