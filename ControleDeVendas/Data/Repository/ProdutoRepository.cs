using ControleDeVendas.Domain.Entities;
using ControleDeVendas.Domain.Interface;

namespace ControleDeVendas.Data.Repository
{
	public class ProdutoRepository : IProdutoRepository
	{
		private static readonly List<Produto> _produtos = new();

		public Produto? ObterPorId(Guid id) => _produtos.FirstOrDefault(p => p.Id == id);
		public IEnumerable<Produto> ObterTodos() => _produtos;

		public void Atualizar(Produto produto)
		{
			var index = _produtos.FindIndex(p => p.Id == produto.Id);
			if (index != -1) _produtos[index] = produto;
		}

		public Task<Produto?> BuscarPorIdAsync(Guid id)
		{
			var produto = _produtos.FirstOrDefault(p => p.Id == id);
			return Task.FromResult(produto);
		}

		public Task<IEnumerable<Produto>> ObterTodosAsync()
		{
			return Task.FromResult(_produtos.AsEnumerable());
		}
	}

}
