using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Application.Dtos
{
    public class TotaisTransacoesCategoriaDto
    {
        public string Descricao { get; set; }
        public string Cor { get; set; } 
        public decimal Receitas { get; set; }
        public decimal Despesas { get; set; }
        public decimal Saldo => Receitas - Despesas;
    }
}
