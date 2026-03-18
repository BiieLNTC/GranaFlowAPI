using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Application.Dtos
{
    public class TotaisTransacoesDto
    {
        public decimal SaldoTotal { get; set; } 
        public decimal SaldoReceitasMes { get; set; }
        public decimal TotalReceitasMes { get; set; }
        public decimal SaldoDespesasMes { get; set; }    
        public decimal TotalDespesasMes { get; set; }    
    }
}
