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
        public int Id { get; set; } 
        public int UsuarioId { get; set; } 
        public string Descricao { get; set; }        
        public ETipoCategoria Finalidade { get; set; }
        public string Cor { get; set; }
        public DateTime CadastradoEm { get; set; } = DateTime.UtcNow;

        public void Atualizar(string descricao, ETipoCategoria finalidade, string cor)
        {
            Descricao = descricao;
            Finalidade = finalidade;
            Cor = cor;
        }
    }
}
