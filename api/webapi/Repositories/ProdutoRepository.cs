using Microsoft.EntityFrameworkCore;
using webapi.Context;
using webapi.Models;

namespace webapi.Repositories
{
    public class ProdutoRepository
    {
        private readonly ApiContext _context;

        public ProdutoRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> ObterProdutos()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<Produto> ObterProdutoPorId(int id)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Produto> ObterProdutoPorSku(string sku)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Sku.Trim().ToUpper() == sku.Trim().ToUpper());
        }

        public async Task AdicionarProduto(Produto produto)
        {
            produto.Id = 0;
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }
        

        public async Task AtualizarProduto(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverProduto(int id)
        {
            var produto = await ObterProdutoPorId(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}
