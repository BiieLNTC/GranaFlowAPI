using GranaFlow.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GranaFlow.Domain.Entities
{
    public class Transacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Usuario))]
        public int UsuarioId { get; set; }

        [Required]
        [ForeignKey(nameof(Categoria))]
        public int CategoriaId { get; set; }

        [Required]
        [ForeignKey(nameof(Pessoa))]
        public int PessoaId { get; set; }

        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; }

        [Required]
        public ETipoTransacao Tipo { get; set; }

        [Required]
        [Precision(20, 4)]
        public decimal Valor { get; set; }

        [Required]
        public DateTime CadastradoEm { get; set; } = DateTime.UtcNow;
    }
}
