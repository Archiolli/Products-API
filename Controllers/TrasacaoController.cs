using Inventory.Data;
using Inventory.Models;
using Inventory.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
    [ApiController]
    public class TrasacaoController : ControllerBase
    {
        [HttpGet("/transacoes")]//todas trasaçoes
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context
            )
        {
            return Ok(await context.Transacoes.ToListAsync());
        }

        [HttpGet("/transacoes/{id:int}")]//trasaçoes por id do produto
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var transacoes = await context.Transacoes
                .Where(t => t.ProdutoId == id)
                .ToListAsync();

            if (transacoes is null) return NotFound();

            return Ok(transacoes);
        }

        [HttpPost("/adicionar-estoque/{produtoId:int}")]
        public async Task<IActionResult> AdicionarEstoqueAsync(
            [FromRoute] int produtoId,
            [FromBody] AddEstoqueViewModel model,
            [FromServices] AppDbContext context)
        {
            var produto = await context.Produtos.FindAsync(produtoId);

            if (produto is null) return NotFound(); //verifica se o produto existe
        
            // Verifique se a quantidade é positiva.
            if (model.Quantidade <= 0)
                return BadRequest("Informe uma quantidade positiva para adicionar!");

            produto.Quantidade += model.Quantidade;//add a quantidade
            context.Produtos.Update(produto);

            var transacao = new Transacao //cria nova transação com os dados do produto
            {
                ProdutoId = produtoId,
                Tipo = TipoTransacao.Entrada,
                Quantidade = model.Quantidade,
                Data = DateTime.Now
            };

            
            await context.Transacoes.AddAsync(transacao); //adiciona a transação
            await context.SaveChangesAsync();  //salva os dados

            return Ok(produto);
        }

        [HttpPost("/remover-estoque/{produtoId:int}")]
        public async Task<IActionResult> RemoverEstoqueAsync(
            [FromRoute] int produtoId,
            [FromBody] RemoveEstoqueViewModel model,
            [FromServices] AppDbContext context)
        {
            var produto = await context.Produtos.FindAsync(produtoId);

            if (produto is null) return NotFound(); //verifica se o produto existe
        
            // Verifique se a quantidade é positiva.
            if (model.Quantidade <= 0)
                return BadRequest("Informe uma quantidade positiva para remover!");
            else if(model.Quantidade > produto.Quantidade)
                return BadRequest("Não há essa quantidade em estoque para ser removido!");
            
            produto.Quantidade -= model.Quantidade;//add a quantidade
            context.Produtos.Update(produto);

            var transacao = new Transacao //cria nova transação com os dados do produto
            {
                ProdutoId = produtoId,
                Tipo = TipoTransacao.Entrada,
                Quantidade = model.Quantidade,
                Data = DateTime.Now
            };

            
            await context.Transacoes.AddAsync(transacao); //adiciona a transação
            await context.SaveChangesAsync();  //salva os dados

            return Ok(produto);
        }
    }
}