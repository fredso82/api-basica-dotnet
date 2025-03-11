using webapi.Models;
using webapi.Repositories;

namespace webapi.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoService(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<string> AdicionarProduto(Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Sku))
                return "O SKU é obrigatório";

            if (string.IsNullOrWhiteSpace(produto.Descricao))
                return "A descrição é obrigatória";

            var produtoExistente = await _produtoRepository.ObterProdutoPorSku(produto.Sku);
            if (produtoExistente != null)
                return "Já existe um produto com o SKU informado";

            await _produtoRepository.AdicionarProduto(produto);

            return "";
        }
    }
}
