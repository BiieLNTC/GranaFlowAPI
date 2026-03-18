using GranaFlow.Domain.Entities;
using GranaFlow.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GranaFlow.Application.Dtos
{
    public class TransacaoDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int CategoriaId { get; set; }
        public string NomeCategoria { get; set; }   
        public string CorCategoria { get; set; }
        public int PessoaId { get; set; }
        public string NomePessoa { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Descricao { get; set; }
        public ETipoTransacao Tipo { get; set; }
        public decimal Valor { get; set; }

        public TransacaoDto(int id, int usuarioId, int categoriaId, string nomeCategoria, string corCategoria, int pessoaId, string nomePessoa, DateTime dataTransacao, string descricao, ETipoTransacao tipo, decimal valor)
        {
            Id = id;
            UsuarioId = usuarioId;
            CategoriaId = categoriaId;
            NomeCategoria = nomeCategoria;
            CorCategoria = corCategoria;
            PessoaId = pessoaId;
            NomePessoa = nomePessoa;
            DataTransacao = dataTransacao;
            Descricao = descricao;
            Tipo = tipo;
            Valor = valor;
        }
    }
}
