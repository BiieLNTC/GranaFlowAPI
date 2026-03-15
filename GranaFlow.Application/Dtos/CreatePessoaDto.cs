using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GranaFlow.Application.Dtos
{
    public class CreatePessoaDto
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
