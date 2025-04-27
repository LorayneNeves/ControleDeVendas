using ControleDeVendas.Domain.Entities;

namespace ControleDeVendas.Domain.Interface
{
    public interface IProdutoRepository
    {
        Produto? ObterPorId(Guid id);
        IEnumerable<Produto> ObterTodos();
        void Atualizar(Produto produto);
		Task<Produto?> BuscarPorIdAsync(Guid id);
		Task<IEnumerable<Produto>> ObterTodosAsync();

	}
}
