using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Application.Dtos
{
    public class FiltroTransacaoDto
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public IEnumerable<int> ListUsuariosId { get; set; }
        public IEnumerable<int> ListCategoriasId { get; set; }
    }
}
