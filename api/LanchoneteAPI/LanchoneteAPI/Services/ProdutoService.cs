using LanchoneteAPI.DTOs;
using LanchoneteAPI.Models;
using LanchoneteAPI.Repositories;

namespace LanchoneteAPI.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repo;
    public ProdutoService(IProdutoRepository repo) => _repo = repo;

    private static ProdutoDto ToDto(Produto p) =>
        new(p.Id, p.Nome, p.Descricao, p.Preco,
            p.CategoriaId, p.Categoria?.Nome ?? "",
            p.EstoqueQuantidade, p.EstoqueMinimo,
            p.Disponivel, p.Ativo,
            p.EstoqueQuantidade <= p.EstoqueMinimo);

    public async Task<List<ProdutoDto>> GetAllAsync() =>
        (await _repo.GetAllAsync()).Select(ToDto).ToList();

    public async Task<ProdutoDto?> GetByIdAsync(int id)
    { var p = await _repo.GetByIdAsync(id); return p is null ? null : ToDto(p); }

    public async Task<List<ProdutoDto>> GetEstoqueBaixoAsync() =>
        (await _repo.GetEstoqueBaixoAsync()).Select(ToDto).ToList();

    public async Task<ProdutoDto> CreateAsync(CreateProdutoRequest r)
    {
        var p = await _repo.CreateAsync(new Produto
        {
            Nome = r.Nome,
            Descricao = r.Descricao,
            Preco = r.Preco,
            CategoriaId = r.CategoriaId,
            EstoqueQuantidade = r.EstoqueQuantidade,
            EstoqueMinimo = r.EstoqueMinimo
        });
        return ToDto((await _repo.GetByIdAsync(p.Id))!);
    }

    public async Task<ProdutoDto?> UpdateAsync(int id, UpdateProdutoRequest r)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p is null) return null;
        p.Nome = r.Nome; p.Descricao = r.Descricao; p.Preco = r.Preco;
        p.CategoriaId = r.CategoriaId; p.EstoqueQuantidade = r.EstoqueQuantidade;
        p.EstoqueMinimo = r.EstoqueMinimo; p.Disponivel = r.Disponivel; p.Ativo = r.Ativo;
        return ToDto(await _repo.UpdateAsync(p));
    }

    public async Task<ProdutoDto?> AtualizarEstoqueAsync(int id, AtualizarEstoqueRequest r)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p is null) return null;
        p.EstoqueQuantidade = r.Operacao switch
        {
            "adicionar" => p.EstoqueQuantidade + r.Quantidade,
            "remover" => Math.Max(0, p.EstoqueQuantidade - r.Quantidade),
            _ => r.Quantidade
        };
        return ToDto(await _repo.UpdateAsync(p));
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p is null) return false;
        await _repo.DeleteAsync(id);
        return true;
    }
}