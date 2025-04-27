using ControleDeVendas.Domain.Entities;
using ControleDeVendas.Domain.Interface;

namespace ControleDeVendas.Application.Services
{
	public class ProdutoService
	{
		private readonly IProdutoRepository _produtoRepository;

		public ProdutoService(IProdutoRepository produtoRepository)
		{
			_produtoRepository = produtoRepository;
		}

		public async Task<Produto> BuscarPorIdAsync(Guid id)
		{
			var produto = await _produtoRepository.BuscarPorIdAsync(id);

			if (produto == null)
				throw new InvalidOperationException("Produto não encontrado");

			return produto;
		}

		public async Task<IEnumerable<Produto>> ListarProdutosAsync()
		{
			var produtos = await _produtoRepository.ObterTodosAsync();
			return produtos;
		}
	}


}
