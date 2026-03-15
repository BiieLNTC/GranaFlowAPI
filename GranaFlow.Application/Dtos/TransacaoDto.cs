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
        public int PessoaId { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Descricao { get; set; }
        public ETipoTransacao Tipo { get; set; }
        public decimal Valor { get; set; }
    }
}
