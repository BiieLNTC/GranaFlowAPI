using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GranaFlow.Application.Dtos
{
    public class UpdatePessoaDto
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
