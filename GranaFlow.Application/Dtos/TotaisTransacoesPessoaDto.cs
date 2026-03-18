using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Application.Dtos
{
    public class TotaisTransacoesPessoaDto
    {
        public string Nome { get; set; }
        public decimal Receitas { get; set; }
        public decimal Despesas { get; set; }
        public decimal Saldo => Receitas - Despesas;
    }
}
