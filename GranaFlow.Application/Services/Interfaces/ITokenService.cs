using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(int id, string nome, string email, DateTime cadastradoEm);
    }
}
