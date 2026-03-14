using GranaFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<bool> ExistUser(string email);
        Task<bool> CreateUser(Usuario user);
        Task<Usuario> GetByEmailAndPassword(string email, string senha);
    }
}
