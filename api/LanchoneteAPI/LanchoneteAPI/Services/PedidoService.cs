using LanchoneteAPI.DTOs;
using LanchoneteAPI.Models;
using LanchoneteAPI.Repositories;

namespace LanchoneteAPI.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _repo;
    private readonly IProdutoRepository _produtoRepo;
    private readonly ICupomRepository _cupomRepo;

    public PedidoService(IPedidoRepository repo, IProdutoRepository produtoRepo, ICupomRepository cupomRepo)
    { _repo = repo; _produtoRepo = produtoRepo; _cupomRepo = cupomRepo; }

    private static PedidoDto ToDto(Pedido p) => new(
        p.Id, p.UsuarioId, p.Usuario?.Nome,
        p.Status.ToString(), p.Tipo.ToString(), p.FormaPagamento.ToString(),
        p.Subtotal, p.Desconto, p.TaxaEntrega, p.Total,
        p.Cupom?.Codigo, p.NomeCliente, p.TelefoneCliente,
        p.EnderecoEntrega, p.Observacao, p.CriadoEm, p.FinalizadoEm,
        p.Itens.Select(i => new ItemPedidoDto(i.Id, i.ProdutoId, i.Produto?.Nome ?? "", i.Quantidade, i.PrecoUnitario, i.Subtotal, i.Observacao)).ToList()
    );

    public async Task<List<PedidoDto>> GetAllAsync(DateTime? ini, DateTime? fim, string? status) =>
        (await _repo.GetAllAsync(ini, fim, status)).Select(ToDto).ToList();

    public async Task<PedidoDto?> GetByIdAsync(int id)
    { var p = await _repo.GetByIdAsync(id); return p is null ? null : ToDto(p); }

    public async Task<PedidoDto> CreateAsync(CreatePedidoRequest r)
    {
        var itens = new List<ItemPedido>();
        decimal subtotal = 0;

        foreach (var item in r.Itens)
        {
            var produto = await _produtoRepo.GetByIdAsync(item.ProdutoId)
                ?? throw new Exception($"Produto {item.ProdutoId} não encontrado.");
            if (!produto.Disponivel || !produto.Ativo)
                throw new Exception($"Produto '{produto.Nome}' não está disponível.");
            if (produto.EstoqueQuantidade < item.Quantidade)
                throw new Exception($"Estoque insuficiente para '{produto.Nome}'.");

            var sub = produto.Preco * item.Quantidade;
            subtotal += sub;
            itens.Add(new ItemPedido
            {
                ProdutoId = item.ProdutoId,
                Quantidade = item.Quantidade,
                PrecoUnitario = produto.Preco,
                Subtotal = sub,
                Observacao = item.Observacao
            });
            produto.EstoqueQuantidade -= item.Quantidade;
            await _produtoRepo.UpdateAsync(produto);
        }

        decimal desconto = 0;
        int? cupomId = null;
        if (!string.IsNullOrWhiteSpace(r.CupomCodigo))
        {
            var cupom = await _cupomRepo.GetByCodigoAsync(r.CupomCodigo);
            if (cupom is not null && cupom.Ativo)
            {
                desconto = cupom.TipoDesconto == TipoDesconto.Percentual
                    ? subtotal * cupom.ValorDesconto / 100
                    : cupom.ValorDesconto;
                desconto = Math.Min(desconto, subtotal);
                cupomId = cupom.Id;
                cupom.UsosAtuais++;
                await _cupomRepo.UpdateAsync(cupom);
            }
        }

        var taxaEntrega = r.Tipo == "Delivery" ? 8m : 0m;
        var pedido = await _repo.CreateAsync(new Pedido
        {
            UsuarioId = r.UsuarioId,
            Tipo = Enum.Parse<TipoPedido>(r.Tipo, true),
            FormaPagamento = Enum.Parse<FormaPagamento>(r.FormaPagamento, true),
            Subtotal = subtotal,
            Desconto = desconto,
            TaxaEntrega = taxaEntrega,
            Total = subtotal - desconto + taxaEntrega,
            CupomId = cupomId,
            NomeCliente = r.NomeCliente,
            TelefoneCliente = r.TelefoneCliente,
            EnderecoEntrega = r.EnderecoEntrega,
            Observacao = r.Observacao,
            Itens = itens
        });

        return ToDto((await _repo.GetByIdAsync(pedido.Id))!);
    }

    public async Task<PedidoDto?> AtualizarStatusAsync(int id, AtualizarStatusRequest r)
    {
        var pedido = await _repo.GetByIdAsync(id);
        if (pedido is null) return null;
        pedido.Status = Enum.Parse<StatusPedido>(r.Status, true);
        if (pedido.Status == StatusPedido.Entregue || pedido.Status == StatusPedido.Cancelado)
            pedido.FinalizadoEm = DateTime.UtcNow;
        return ToDto(await _repo.UpdateAsync(pedido));
    }

    public async Task<bool> CancelarAsync(int id)
    {
        var pedido = await _repo.GetByIdAsync(id);
        if (pedido is null) return false;
        foreach (var item in pedido.Itens)
        {
            var produto = await _produtoRepo.GetByIdAsync(item.ProdutoId);
            if (produto is not null)
            { produto.EstoqueQuantidade += item.Quantidade; await _produtoRepo.UpdateAsync(produto); }
        }
        pedido.Status = StatusPedido.Cancelado;
        pedido.FinalizadoEm = DateTime.UtcNow;
        await _repo.UpdateAsync(pedido);
        return true;
    }
}