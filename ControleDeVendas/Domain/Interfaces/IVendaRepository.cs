using ControleDeVendas.Domain.Entities;

namespace ControleDeVendas.Domain.Interface
{
	public interface IVendaRepository
	{
		Task Adicionar(Venda venda);
		Venda? ObterPorId(Guid id);
	}
}
