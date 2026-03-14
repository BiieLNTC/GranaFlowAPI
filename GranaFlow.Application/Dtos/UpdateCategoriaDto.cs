using GranaFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GranaFlow.Application.Dtos
{
    public class UpdateCategoriaDto
    {
        public int Id { get; set; }        
        public string Descricao { get; set; }
        public ETipoCategoria Finalidade { get; set; }
        public string Cor { get; set; }
    }
}
