using GranaFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GranaFlow.Domain.Entities
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Usuario))]
        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; }

        [Required]
        public ETipoCategoria Finalidade { get; set; }

        [Required]
        [MaxLength(150)]
        public string Cor { get; set; }

        [Required]
        public DateTime CadastradoEm { get; set; } = DateTime.UtcNow;
    }
}
