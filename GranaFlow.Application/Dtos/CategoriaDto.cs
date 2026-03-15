using GranaFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Application.Dtos
{
    public class CategoriaDto
    {
        public int Id { get; set; } // Key
        public int UsuarioId { get; set; } // Foreign Key - Usuario
        public string Descricao { get; set; }
        public ETipoCategoria Finalidade { get; set; }
        public string Cor { get; set; }

        public CategoriaDto(int id, int usuarioId, string descricao, ETipoCategoria finalidade, string cor)
        {
            Id = id;
            UsuarioId = usuarioId;
            Descricao = descricao;
            Finalidade = finalidade;
            Cor = cor;
        }
    }
}
