using GranaFlow.Application.Dtos;
using GranaFlow.Application.Services.Interfaces;
using GranaFlow.Application.Utils;
using GranaFlow.Domain.Entities;
using GranaFlow.Domain.Interfaces.Repositories;

namespace GranaFlow.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly CryptoUtils _cryptoUtils;
        private readonly ITokenService _tokenService;

        public UsuarioService(IUsuarioRepository repo, CryptoUtils cryptoUtils, ITokenService tokenService)
        {
            _repo = repo;
            _cryptoUtils = cryptoUtils;
            _tokenService = tokenService;
        }

        public async Task<bool> CreateAccount(CreateUserDto login)
        {
            var existingUser = await _repo.ExistUser(login.Email);
            if (existingUser)
            {
                throw new Exception("Esse e-mail já foi cadastrado"); ;
            }

            var senhaCrypto = _cryptoUtils.EncryptString(login.Senha);

            var user = new Usuario
            {
                Nome = login.Nome,
                Email = login.Email,
                SenhaHash = senhaCrypto
            };

            var result = await _repo.CreateUser(user);

            return result;
        }

        public async Task<string> Login(LoginDto login) 
        {
            var senhaCrypto = _cryptoUtils.EncryptString(login.Senha);
            var usuario = await _repo.GetByEmailAndPassword(login.Email, senhaCrypto);

            if (usuario is null)
                throw new Exception("E-mail ou senha inválidos.");

            var token = _tokenService.GerarToken(usuario.Id, usuario.Nome, usuario.Email, usuario.CadastradoEm);
            return token;
        }
    }
}
