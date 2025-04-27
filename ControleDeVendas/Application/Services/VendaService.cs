using ControleDeVendas.Domain.Entities;
using ControleDeVendas.Domain.Interface;

namespace ControleDeVendas.Application.Services
{
    public class VendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IProdutoRepository _produtoRepository;

        public VendaService(IVendaRepository vendaRepository, IProdutoRepository produtoRepository)
        {
            _vendaRepository = vendaRepository;
            _produtoRepository = produtoRepository;
        }

        public void RealizarVenda(Venda venda)
        {
            foreach (var item in venda.Itens)
            {
                var produto = _produtoRepository.ObterPorId(item.Produto.Id);
                if (produto == null || produto.Estoque < item.Quantidade)
                    throw new InvalidOperationException("Estoque insuficiente");

                produto.Estoque -= item.Quantidade;
                _produtoRepository.Atualizar(produto);
            }

            _vendaRepository.Adicionar(venda);
        }
    }
}
