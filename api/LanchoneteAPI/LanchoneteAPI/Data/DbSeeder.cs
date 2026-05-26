using LanchoneteAPI.Models;

namespace LanchoneteAPI.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!db.Usuarios.Any())
        {
            db.Usuarios.AddRange(
                new Usuario
                {
                    Nome = "Administrador",
                    Email = "admin@lanchonete.com",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Perfil = PerfilUsuario.Admin,
                    Telefone = "(11) 99999-0001"
                },
                new Usuario
                {
                    Nome = "Funcionário Padrão",
                    Email = "funcionario@lanchonete.com",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("Func@123"),
                    Perfil = PerfilUsuario.Funcionario,
                    Telefone = "(11) 99999-0002"
                }
            );
            await db.SaveChangesAsync();
        }

        if (!db.Categorias.Any())
        {
            db.Categorias.AddRange(
                new Categoria { Nome = "Lanches", Descricao = "Hambúrgueres, sanduíches e wraps" },
                new Categoria { Nome = "Bebidas", Descricao = "Sucos, refrigerantes e shakes" },
                new Categoria { Nome = "Porcões", Descricao = "Batata frita, onion rings e petiscos" },
                new Categoria { Nome = "Sobremesas", Descricao = "Sorvetes, brownies e doces" },
                new Categoria { Nome = "Combos", Descricao = "Lanche + bebida + porcão" }
            );
            await db.SaveChangesAsync();
        }

        if (!db.Produtos.Any())
        {
            var idLanches = db.Categorias.First(c => c.Nome == "Lanches").Id;
            var idBebidas = db.Categorias.First(c => c.Nome == "Bebidas").Id;
            var idPorcoes = db.Categorias.First(c => c.Nome == "Porcões").Id;
            var idSobrem = db.Categorias.First(c => c.Nome == "Sobremesas").Id;
            var idCombos = db.Categorias.First(c => c.Nome == "Combos").Id;

            db.Produtos.AddRange(
                new Produto { Nome = "X-Burguer", Descricao = "Hambúrguer artesanal, queijo, alface e tomate", Preco = 22.90m, CategoriaId = idLanches, EstoqueQuantidade = 50, EstoqueMinimo = 10 },
                new Produto { Nome = "X-Bacon", Descricao = "Hambúrguer duplo com bacon crocante e cheddar", Preco = 28.90m, CategoriaId = idLanches, EstoqueQuantidade = 40, EstoqueMinimo = 10 },
                new Produto { Nome = "X-Frango", Descricao = "Frango grelhado, cream cheese e rúcula", Preco = 24.90m, CategoriaId = idLanches, EstoqueQuantidade = 45, EstoqueMinimo = 10 },
                new Produto { Nome = "Veggie Burguer", Descricao = "Hambúrguer de grão-de-bico com abacate", Preco = 26.90m, CategoriaId = idLanches, EstoqueQuantidade = 30, EstoqueMinimo = 5 },
                new Produto { Nome = "Coca-Cola 350ml", Descricao = "Refrigerante gelado lata", Preco = 6.00m, CategoriaId = idBebidas, EstoqueQuantidade = 120, EstoqueMinimo = 24 },
                new Produto { Nome = "Suco de Laranja", Descricao = "Suco natural 400ml", Preco = 9.00m, CategoriaId = idBebidas, EstoqueQuantidade = 60, EstoqueMinimo = 15 },
                new Produto { Nome = "Água 500ml", Descricao = "Água mineral com ou sem gás", Preco = 4.00m, CategoriaId = idBebidas, EstoqueQuantidade = 100, EstoqueMinimo = 30 },
                new Produto { Nome = "Milk-Shake", Descricao = "Shake cremoso de chocolate 400ml", Preco = 14.90m, CategoriaId = idBebidas, EstoqueQuantidade = 40, EstoqueMinimo = 10 },
                new Produto { Nome = "Batata Média", Descricao = "Batata frita crocante com sal e ketchup", Preco = 12.00m, CategoriaId = idPorcoes, EstoqueQuantidade = 80, EstoqueMinimo = 20 },
                new Produto { Nome = "Batata Grande", Descricao = "Batata frita porção grande", Preco = 18.00m, CategoriaId = idPorcoes, EstoqueQuantidade = 70, EstoqueMinimo = 15 },
                new Produto { Nome = "Onion Rings", Descricao = "Anéis de cebola empanados crocantes", Preco = 16.00m, CategoriaId = idPorcoes, EstoqueQuantidade = 50, EstoqueMinimo = 10 },
                new Produto { Nome = "Brownie c/ Sorvete", Descricao = "Brownie quente com sorvete de baunilha", Preco = 16.90m, CategoriaId = idSobrem, EstoqueQuantidade = 30, EstoqueMinimo = 5 },
                new Produto { Nome = "Sundae Morango", Descricao = "Sorvete com calda de morango e granola", Preco = 12.90m, CategoriaId = idSobrem, EstoqueQuantidade = 35, EstoqueMinimo = 5 },
                new Produto { Nome = "Combo Clássico", Descricao = "X-Burguer + Batata Média + Refri 350ml", Preco = 35.90m, CategoriaId = idCombos, EstoqueQuantidade = 50, EstoqueMinimo = 10 },
                new Produto { Nome = "Combo Premium", Descricao = "X-Bacon + Batata Grande + Milk-Shake", Preco = 52.90m, CategoriaId = idCombos, EstoqueQuantidade = 30, EstoqueMinimo = 5 }
            );
            await db.SaveChangesAsync();
        }

        if (!db.Cupons.Any())
        {
            db.Cupons.AddRange(
                new Cupom { Codigo = "BEMVINDO10", Descricao = "10% de desconto para novos clientes", TipoDesconto = TipoDesconto.Percentual, ValorDesconto = 10, LimiteUsos = 100, ValidoAte = DateTime.UtcNow.AddMonths(6) },
                new Cupom { Codigo = "FRETE0", Descricao = "Frete grátis em pedidos acima de R$50", TipoDesconto = TipoDesconto.ValorFixo, ValorDesconto = 8, LimiteUsos = 50, ValidoAte = DateTime.UtcNow.AddMonths(3), ValorMinimoPedido = 50 },
                new Cupom { Codigo = "DESC5", Descricao = "R$5 de desconto em pedidos acima de R$30", TipoDesconto = TipoDesconto.ValorFixo, ValorDesconto = 5, LimiteUsos = 200, ValidoAte = DateTime.UtcNow.AddMonths(12), ValorMinimoPedido = 30 }
            );
            await db.SaveChangesAsync();
        }
    }
}