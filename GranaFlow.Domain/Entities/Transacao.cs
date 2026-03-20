using GranaFlow.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GranaFlow.Domain.Entities
{
    public class Transacao
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Descricao { get; set; }
        public ETipoTransacao Tipo { get; set; }
        public decimal Valor { get; set; }
        public DateTime CadastradoEm { get; set; } = DateTime.UtcNow;

        public void Atualizar(int categoriaId, int pessoaId, DateTime dataTransacao, string descricao, ETipoTransacao tipo, decimal valor)
        {
            CategoriaId = categoriaId;
            PessoaId = pessoaId;
            DataTransacao = dataTransacao;
            Descricao = descricao;
            Tipo = tipo;
            Valor = valor;
        }
    }
}
