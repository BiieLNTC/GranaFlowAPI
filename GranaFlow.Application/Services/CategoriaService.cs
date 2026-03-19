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
                throw new Exception("Já existe uma categoria com esse nome cadastrada!");
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
            var existCategoria = await _repo.ExistsAsync(categoriaDto.Descricao, categoriaDto.Id);

            if (existCategoria)
            {
                throw new Exception("Já existe uma categoria com esse nome cadastrada!"); ;
            }

            var categoriaToUpdate = await _repo.GetByIdAsync(categoriaDto.Id);

            if (categoriaToUpdate == null)
            {
                throw new InvalidOperationException("Categoria não encontrada.");
            }

            categoriaToUpdate.Atualizar(categoriaDto.Descricao, categoriaDto.Finalidade, categoriaDto.Cor);

            var result = await _repo.UpdateAsync(categoriaToUpdate);

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoria = await _repo.GetByIdAsync(id);

            if (categoria == null)
                throw new InvalidOperationException("Categoria não encontrada.");

            return await _repo.DeleteAsync(id);
        }

        public async Task<CategoriaDto> GetByIdAsync(int id)
        {
            var categoria = await _repo.GetByIdAsync(id);
            if (categoria == null)
                throw new InvalidOperationException("Categoria não encontrada.");

            return new CategoriaDto(categoria.Id,
                                    categoria.UsuarioId,
                                    categoria.Descricao,
                                    categoria.Finalidade,
                                    categoria.Cor);
        }

        public async Task<List<CategoriaDto>> GetAllAsync()
        {
            var categorias = await _repo.GetAllAsync();

            return categorias.Select(s => new CategoriaDto(s.Id,
                                                            s.UsuarioId,
                                                            s.Descricao,
                                                            s.Finalidade,
                                                            s.Cor)).ToList();
        }
    }
}
