using GranaFlow.Application.Auth;
using GranaFlow.Application.Dtos;
using GranaFlow.Domain.Entities;
using GranaFlow.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranaFlow.Application.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _repo;
        public readonly InfoToken _infoToken;

        public CategoriaService(ICategoriaRepository repo, InfoToken infoToken)
        {
            _repo = repo;
            _infoToken = infoToken;
        }

        public async Task<bool> CreateAsync(CreateCategoriaDto categoriaDto)
        {
            var existCategoria = await _repo.ExistsAsync(categoriaDto.Descricao);

            if (existCategoria)
            {
                throw new Exception("Já existe uma categoria com esse nome cadastrada!"); ;
            }

            var categoria = new Categoria()
            {
                UsuarioId = _infoToken.Id,
                Descricao = categoriaDto.Descricao,
                Finalidade = categoriaDto.Finalidade,
                Cor = categoriaDto.Cor
            };

            var result = await _repo.CreateAsync(categoria);

            return result;
        }

        public async Task<bool> UpdateAsync(UpdateCategoriaDto categoriaDto)
        {
            var existCategoria = await _repo.ExistsAsync(categoriaDto.Descricao);

            if (existCategoria)
            {
                throw new Exception("Já existe uma categoria com esse nome cadastrada!"); ;
            }

            var result = await _repo.UpdateASync(categoriaDto);

            return result;
        }
    }
}
