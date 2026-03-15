using GranaFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Application.Dtos
{
    public record UpdateTransacaoDto
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public int PessoaId { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Descricao { get; set; }
        public ETipoTransacao Tipo { get; set; }

        public decimal Valor { get; set; }
    }
}
