using GranaFlow.Application.Auth;
using GranaFlow.Application.Dtos;
using GranaFlow.Domain.Entities;
using GranaFlow.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Application.Services
{
    public class PessoaService
    {
        private readonly IPessoaRepository _repo;
        public readonly InfoToken _infoToken;

        public PessoaService(IPessoaRepository repo, InfoToken infoToken)
        {
            _repo = repo;
            _infoToken = infoToken;
        }

        public async Task<bool> CreateAsync(CreatePessoaDto pessoaDto)
        {
            var existPessoa = await _repo.ExistsAsync(pessoaDto.Nome);

            if (existPessoa)
            {
                throw new Exception("Já existe uma pessoa com esse nome cadastrada!"); ;
            }

            var pessoa = new Pessoa()
            {
                UsuarioId = _infoToken.Id,
                Nome = pessoaDto.Nome,
                DataNascimento = pessoaDto.DataNascimento
            };

            var result = await _repo.CreateAsync(pessoa);

            return result;
        }

        public async Task<bool> UpdateAsync(UpdatePessoaDto pessoaUpdate)
        {
            var existPessoa = await _repo.ExistsAsync(pessoaUpdate.Nome, pessoaUpdate.Id);

            if (existPessoa)
            {
                throw new Exception("Já existe uma pessoa com esse nome cadastrada!"); ;
            }

            var pessoaToUpdate = await _repo.GetByIdAsync(pessoaUpdate.Id);

            if (pessoaToUpdate == null)
            {
                throw new InvalidOperationException("Pessoa não encontrada.");
            }

            pessoaToUpdate.Atualizar(pessoaUpdate.Nome, pessoaUpdate.DataNascimento);

            var result = await _repo.UpdateAsync(pessoaToUpdate);

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pessoa = await _repo.GetByIdAsync(id);

            if (pessoa == null)
                throw new InvalidOperationException("Pessoa não encontrada.");

            return await _repo.DeleteAsync(id);
        }

        public async Task<PessoaDto> GetByIdAsync(int id)
        {
            var pessoa = await _repo.GetByIdAsync(id);
            if (pessoa == null)
                throw new InvalidOperationException("Pessoa não encontrada.");

            return new PessoaDto(pessoa.Id,
                                    pessoa.UsuarioId,
                                    pessoa.Nome,
                                    pessoa.DataNascimento);
        }

        public async Task<List<PessoaDto>> GetAllAsync()
        {
            var pessoas = await _repo.GetAllAsync();

            return pessoas.Select(s => new PessoaDto(s.Id,
                                                     s.UsuarioId,
                                                     s.Nome,
                                                     s.DataNascimento)).ToList();
        }
    }
}
