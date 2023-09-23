using Inventory.Data;
using Inventory.Models;
using Inventory.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        [HttpGet("/")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context
            )
        {
            return Ok(await context.Produtos.ToListAsync());
        }

        [HttpGet("/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var produto = await context.Produtos.FindAsync(id);

            if (produto is null) return NotFound();

            return Ok(produto);
        }

        [HttpPost("/")]
        public async Task<IActionResult> PostAsync(
            [FromBody] CreateProdutoViewModel model,
            [FromServices] AppDbContext context
            )
        {
            var produto = new Produto
            {
                Nome = model.Nome,
                Desc = model.Desc,
                Quantidade = model.Quantidade,
                Valor = model.Valor,
            };

            await context.Produtos.AddAsync(produto);
            await context.SaveChangesAsync();

            return Created($"/{produto.Id}", produto);
        }


        [HttpPut("/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] Produto umProduto,
            [FromServices] AppDbContext context)
        {
            var produto = await context.Produtos.FindAsync(id);

            if (produto is null) return NotFound();

            produto.Nome = umProduto.Nome;
            produto.Desc = umProduto.Desc;
            produto.Quantidade = umProduto.Quantidade;
            produto.Valor = umProduto.Valor;

            context.Produtos.Update(produto);
            await context.SaveChangesAsync();

            return Ok(produto);
        }

        [HttpDelete("/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var produto = await context.Produtos.FindAsync(id);

            if (produto is null) return NotFound();

            context.Produtos.Remove(produto);
            await context.SaveChangesAsync();

            return Ok("Produto " + produto.Nome + " excluido com sucesso!");
        }


        
    }
}