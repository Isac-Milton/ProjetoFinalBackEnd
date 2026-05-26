using LanchoneteAPI.DTOs;
using LanchoneteAPI.Models;
using LanchoneteAPI.Repositories;

namespace LanchoneteAPI.Services;

public class CupomService : ICupomService
{
    private readonly ICupomRepository _repo;
    public CupomService(ICupomRepository repo) => _repo = repo;

    private static CupomDto ToDto(Cupom c) =>
        new(c.Id, c.Codigo, c.Descricao, c.TipoDesconto.ToString(),
            c.ValorDesconto, c.ValorMinimoPedido, c.LimiteUsos, c.UsosAtuais, c.ValidoAte, c.Ativo);

    public async Task<List<CupomDto>> GetAllAsync() =>
        (await _repo.GetAllAsync()).Select(ToDto).ToList();

    public async Task<CupomDto> CreateAsync(CreateCupomRequest r)
    {
        var tipo = Enum.Parse<TipoDesconto>(r.TipoDesconto, true);
        var c = await _repo.CreateAsync(new Cupom
        {
            Codigo = r.Codigo,
            Descricao = r.Descricao,
            TipoDesconto = tipo,
            ValorDesconto = r.ValorDesconto,
            ValorMinimoPedido = r.ValorMinimoPedido,
            LimiteUsos = r.LimiteUsos,
            ValidoAte = r.ValidoAte
        });
        return ToDto(c);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var c = await _repo.GetByIdAsync(id);
        if (c is null) return false;
        await _repo.DeleteAsync(id);
        return true;
    }

    public async Task<ValidarCupomResponse> ValidarAsync(ValidarCupomRequest r)
    {
        var cupom = await _repo.GetByCodigoAsync(r.Codigo);
        if (cupom is null) return new(false, "Cupom não encontrado.", 0);
        if (!cupom.Ativo) return new(false, "Cupom inativo.", 0);
        if (cupom.ValidoAte.HasValue && cupom.ValidoAte.Value < DateTime.UtcNow)
            return new(false, "Cupom expirado.", 0);
        if (cupom.LimiteUsos.HasValue && cupom.UsosAtuais >= cupom.LimiteUsos.Value)
            return new(false, "Limite de usos atingido.", 0);
        if (cupom.ValorMinimoPedido.HasValue && r.ValorPedido < cupom.ValorMinimoPedido.Value)
            return new(false, $"Pedido mínimo de R${cupom.ValorMinimoPedido:F2}.", 0);

        var desconto = cupom.TipoDesconto == TipoDesconto.Percentual
            ? r.ValorPedido * cupom.ValorDesconto / 100
            : cupom.ValorDesconto;

        return new(true, "Cupom válido!", Math.Min(desconto, r.ValorPedido));
    }
}