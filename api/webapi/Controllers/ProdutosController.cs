using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Repositories;
using webapi.Services;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoRepository _produtoRepository;
    private readonly ProdutoService _produtoService;

    public ProdutosController(ProdutoRepository produtoRepository, ProdutoService produtoService)
    {
        _produtoRepository = produtoRepository;
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> ObterProdutos()
    {
        var produtos = await _produtoRepository.ObterProdutos();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> ObterProdutoPorId(int id)
    {
        var produto = await _produtoRepository.ObterProdutoPorId(id);
        if (produto == null)
        {
            return NotFound();
        }
        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult> AdicionarProduto(Produto produto)
    {
        var retorno = await _produtoService.AdicionarProduto(produto);
        if (!string.IsNullOrWhiteSpace(retorno))
        {
            return BadRequest(retorno);
        }
        
        return Ok(produto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> AtualizarProduto(int id, Produto produto)
    {
        var produtoExistente = await _produtoRepository.ObterProdutoPorId(id);
        if (produtoExistente == null)
        {
            return NotFound();
        }
        produto.Id = id;
        await _produtoRepository.AtualizarProduto(produto);
        return Ok(produto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoverProduto(int id)
    {
        var produto = await _produtoRepository.ObterProdutoPorId(id);
        if (produto == null)
        {
            return NotFound();
        }
        await _produtoRepository.RemoverProduto(id);
        return Ok();
    }
}
