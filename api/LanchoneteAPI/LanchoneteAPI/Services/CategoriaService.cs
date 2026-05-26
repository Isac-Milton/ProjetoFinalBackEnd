using LanchoneteAPI.DTOs;
using LanchoneteAPI.Models;
using LanchoneteAPI.Repositories;

namespace LanchoneteAPI.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _repo;
    public CategoriaService(ICategoriaRepository repo) => _repo = repo;

    private static CategoriaDto ToDto(Categoria c) =>
        new(c.Id, c.Nome, c.Descricao, c.Ativo, c.Produtos.Count(p => p.Ativo));

    public async Task<List<CategoriaDto>> GetAllAsync() =>
        (await _repo.GetAllAsync()).Select(ToDto).ToList();

    public async Task<CategoriaDto?> GetByIdAsync(int id)
    { var c = await _repo.GetByIdAsync(id); return c is null ? null : ToDto(c); }

    public async Task<CategoriaDto> CreateAsync(CreateCategoriaRequest r)
    {
        var c = await _repo.CreateAsync(new Categoria { Nome = r.Nome, Descricao = r.Descricao });
        return ToDto(c);
    }

    public async Task<CategoriaDto?> UpdateAsync(int id, UpdateCategoriaRequest r)
    {
        var c = await _repo.GetByIdAsync(id);
        if (c is null) return null;
        c.Nome = r.Nome; c.Descricao = r.Descricao; c.Ativo = r.Ativo;
        return ToDto(await _repo.UpdateAsync(c));
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var c = await _repo.GetByIdAsync(id);
        if (c is null) return false;
        await _repo.DeleteAsync(id);
        return true;
    }
}