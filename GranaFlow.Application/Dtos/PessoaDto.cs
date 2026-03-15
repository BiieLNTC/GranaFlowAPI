using GranaFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GranaFlow.Application.Dtos
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        public PessoaDto(int id, int usuarioId, string nome, DateTime dataNascimento)
        {
            Id = id;
            UsuarioId = usuarioId;
            Nome = nome;
            DataNascimento = dataNascimento;
        }
    }
}
