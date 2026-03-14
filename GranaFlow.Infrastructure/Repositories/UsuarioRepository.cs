using GranaFlow.Domain.Entities;
using GranaFlow.Domain.Interfaces.Repositories;
using GranaFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly GranaFlowContext _db;

        public UsuarioRepository(GranaFlowContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateUser(Usuario user)
        {
            _db.Usuarios.Add(user);
            var result = await _db.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> ExistUser(string email)
        {
            return await _db.Usuarios.AnyAsync(a => a.Email == email);
        }

        public async Task<Usuario> GetByEmailAndPassword(string email, string senha)
        {
            return await _db.Usuarios.Where(w => EF.Functions.Like(w.Email, email) && w.SenhaHash == senha).FirstOrDefaultAsync();            
        }
    }
}
