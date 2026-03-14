using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GranaFlow.Domain.Entities
{
    [Index(nameof(Email), IsUnique = true)] 
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(300)]
        public string SenhaHash { get; set; } = string.Empty;

        [Required]
        public DateTime CadastradoEm { get; set; } = DateTime.UtcNow;
    }
}
