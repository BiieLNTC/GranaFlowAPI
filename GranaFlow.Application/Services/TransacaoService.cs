using GranaFlow.Application.Auth;
using GranaFlow.Application.Dtos;
using GranaFlow.Domain.Entities;
using GranaFlow.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Application.Services
{
    public class TransacaoService
    {
        private readonly ITransacaoRepository _repo;
        public readonly InfoToken _infoToken;

        public TransacaoService(ITransacaoRepository repo, InfoToken infoToken)
        {
            _repo = repo;
            _infoToken = infoToken;
        }

        public async Task<bool> CreateAsync(CreateTransacaoDto createTransacao)
        {
            var transacao = new Transacao()
            {
                UsuarioId = _infoToken.Id,
                Descricao = createTransacao.Descricao,
                CategoriaId = createTransacao.CategoriaId,
                PessoaId = createTransacao.PessoaId,
                DataTransacao = createTransacao.DataTransacao,
                Tipo = createTransacao.Tipo,
                Valor = createTransacao.Valor,
            };

            var result = await _repo.CreateAsync(transacao);

            return result;
        }

        public async Task<bool> UpdateAsync(UpdateTransacaoDto updateTransacao)
        {
            var transacaoToUpdate = await _repo.GetByIdAsync(updateTransacao.Id);

            if (transacaoToUpdate == null)
            {
                throw new InvalidOperationException("Transação não encontrada.");
            }

            transacaoToUpdate.Atualizar(updateTransacao.CategoriaId,
                                        updateTransacao.PessoaId,
                                        updateTransacao.DataTransacao,
                                        updateTransacao.Descricao,
                                        updateTransacao.Tipo,
                                        updateTransacao.Valor);

            var result = await _repo.UpdateAsync(transacaoToUpdate);

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transcao = await _repo.GetByIdAsync(id);

            if (transcao == null)
                throw new InvalidOperationException("Transação não encontrada.");

            return await _repo.DeleteAsync(id);
        }

        public async Task<PessoaDto> GetByIdAsync(int id)
        {
            var transacao = await _repo.GetByIdAsync(id);
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
