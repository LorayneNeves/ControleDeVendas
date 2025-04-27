using ControleDeVendas.Domain.Entities;
using ControleDeVendas.Domain.Interface;

namespace ControleDeVendas.Data.Repository
{
	public class VendaRepository : IVendaRepository
	{
		private static readonly List<Venda> _vendas = new();

		public Task Adicionar(Venda venda)
		{
			_vendas.Add(venda);
			return Task.CompletedTask;
		}

		public Venda? ObterPorId(Guid id) => _vendas.FirstOrDefault(v => v.Id == id);
	}
}
