using GranaFlow.Application.Auth;
using GranaFlow.Application.Dtos;
using GranaFlow.Domain.Entities;
using GranaFlow.Domain.Enums;
using GranaFlow.Domain.Interfaces.Repositories;
using ToolSharp.Utils;
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

        public async Task<TransacaoDto> GetByIdAsync(int id)
        {
            var transacao = await _repo.GetByIdAsync(id);
            if (transacao == null)
                throw new InvalidOperationException("Transação não encontrada.");

            return new TransacaoDto(transacao.Id,
                                    transacao.UsuarioId,
                                    transacao.CategoriaId,
                                    string.Empty,
                                    string.Empty,
                                    transacao.PessoaId,
                                    string.Empty,
                                    transacao.DataTransacao,
                                    transacao.Descricao,
                                    transacao.Tipo,
                                    transacao.Valor);
        }

        public async Task<List<TransacaoDto>> GetAllAsync(DateTime dataInicial, DateTime dataFinal)
        {
            var transacoes = await _repo.GetAllAsync(dataInicial, dataFinal);

            return transacoes.Select(s => new TransacaoDto(s.Id,
                                                     s.UsuarioId,
                                                     s.CategoriaId,
                                                     s.Categoria.Descricao,
                                                     s.Categoria.Cor,
                                                     s.PessoaId,
                                                     s.Pessoa.Nome,
                                                     s.DataTransacao,
                                                     s.Descricao,
                                                     s.Tipo,
                                                     s.Valor)).ToList();
        }

        public async Task<List<TotaisTransacoesPessoaDto>> ObterTotaisPessoas()
        {
            var groupTransacoes = await _repo.GetTransacoesPorPessoa();

            return groupTransacoes.Select(g => new TotaisTransacoesPessoaDto
            {
                Nome = g.Select(s => s.Pessoa.Nome).FirstOrDefault(),
                Receitas = g.Where(t => t.Tipo == ETipoTransacao.Receita).Sum(t => t.Valor),
                Despesas = g.Where(t => t.Tipo == ETipoTransacao.Despesa).Sum(t => t.Valor),
            }).ToList();
        }

        public async Task<List<TotaisTransacoesCategoriaDto>> ObterTotaisCategorias()
        {
            var groupTransacoes = await _repo.GetTransacoesPorCategoria();

            return groupTransacoes.Select(g => new TotaisTransacoesCategoriaDto
            {
                Descricao = g.Select(s => s.Categoria.Descricao).FirstOrDefault(),
                Cor = g.Select(s => s.Categoria.Cor).FirstOrDefault(),
                Receitas = g.Where(t => t.Tipo == ETipoTransacao.Receita).Sum(t => t.Valor),
                Despesas = g.Where(t => t.Tipo == ETipoTransacao.Despesa).Sum(t => t.Valor),
            }).ToList();
        }

        public async Task<TotaisTransacoesDto> ObterTotaisTransacoes()
        {
            var dataInicio = DateExtensions.StartOfMonth(DateTime.Now);
            var dataFim = DateExtensions.EndOfMonth(DateTime.Now);

            var listTransacoesMes = await _repo.ObterTransacoesByData(dataInicio, dataFim);
            var saldoTotal = await _repo.ObterSaldoTotal();

            decimal saldoDespesas = 0;
            decimal saldoReceitas = 0;
            int totalDespesas = 0;
            int totalReceitas = 0;

            foreach (var t in listTransacoesMes)
            {
                if (t.Tipo == ETipoTransacao.Despesa)
                {
                    saldoDespesas += t.Valor;
                    totalDespesas++;
                }
                else
                {
                    saldoReceitas += t.Valor;
                    totalReceitas++;
                }
            }

            return new TotaisTransacoesDto
            {
                SaldoDespesasMes = saldoDespesas,
                TotalDespesasMes = totalDespesas,
                SaldoReceitasMes = saldoReceitas,
                TotalReceitasMes = totalReceitas,
                SaldoTotal = saldoTotal
            };
        }

        public async Task<List<TopDespesasDto>> ObterTopDespesas()
        {
            var transacoes = await _repo.GetAllDespesas();

            return transacoes
                .Where(t => t.UsuarioId == _infoToken.Id)
                .GroupBy(g => g.CategoriaId)
                .Select(s => new TopDespesasDto
                {
                    Cor = s.Select(s => s.Categoria.Cor).FirstOrDefault(),
                    NomeCategoria = s.Select(s => s.Categoria.Descricao).FirstOrDefault(),
                    Valor = s.Sum(t => t.Valor)
                })
                .OrderByDescending(x => x.Valor)
                .Take(5)
                .ToList();
        }

        public async Task<List<TopreceitasDto>> ObterTopReceitas()
        {
            var transacoes = await _repo.GetAllReceitas();

            return transacoes
                .Where(t => t.UsuarioId == _infoToken.Id)
                .GroupBy(g => g.CategoriaId)
                .Select(s => new TopreceitasDto
                {
                    Cor = s.Select(s => s.Categoria.Cor).FirstOrDefault(),
                    NomeCategoria = s.Select(s => s.Categoria.Descricao).FirstOrDefault(),
                    Valor = s.Sum(t => t.Valor)
                })
                .OrderByDescending(x => x.Valor)
                .Take(5)
                .ToList();
        }
    }
}
