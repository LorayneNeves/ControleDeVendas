using Bogus;
using ControleDeVendas.Application.Services;
using ControleDeVendas.Data.Repository;
using ControleDeVendas.Domain.Entities;
using ControleDeVendas.Domain.Interface;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace ControleDeVendas.Testes
{
	public class VendaServiceTests
	{
		private readonly IVendaRepository _vendaRepositoryMock;
		private readonly IProdutoRepository _produtoRepositoryMock;
		private readonly VendaService _vendaService;
		private readonly Faker<Produto> _produtoFaker;
		private readonly Faker<Venda> _vendaFaker;

		public VendaServiceTests()
		{
			_vendaRepositoryMock = Substitute.For<IVendaRepository>();
			_produtoRepositoryMock = Substitute.For<IProdutoRepository>();

			_vendaService = new VendaService(_vendaRepositoryMock, _produtoRepositoryMock);

			_produtoFaker = new Faker<Produto>()
				.RuleFor(p => p.Id, f => Guid.NewGuid())
				.RuleFor(p => p.Nome, f => f.Commerce.ProductName())
				.RuleFor(p => p.Preco, f => f.Random.Decimal(1, 1000))
				.RuleFor(p => p.Estoque, f => f.Random.Int(1, 100));

			_vendaFaker = new Faker<Venda>()
				.RuleFor(v => v.Id, f => Guid.NewGuid())
				.RuleFor(v => v.Desconto, f => f.Random.Decimal(0, 0.2m)) 
				.RuleFor(v => v.Itens, f => new List<ItemVenda> {
				new ItemVenda
				{
					Produto = _produtoFaker.Generate(),
					Quantidade = f.Random.Int(1, 10)
				}
				});
		}

		[Fact]
		public void RealizarVenda_DeveRealizarVendaComSucesso()
		{
			var produto = new Produto { Id = Guid.NewGuid(), Nome = "Produto 1", Preco = 100, Estoque = 10 };
			var itemVenda = new ItemVenda { Produto = produto, Quantidade = 2 };
			var venda = new Venda { Id = Guid.NewGuid(), Itens = new List<ItemVenda> { itemVenda } };

			_produtoRepositoryMock.ObterPorId(produto.Id).Returns(produto);
			_vendaRepositoryMock.Adicionar(Arg.Any<Venda>());

			_vendaService.RealizarVenda(venda);

			produto.Estoque.Should().Be(8);
		}


		[Fact]
		public void RealizarVenda_DeveLancarErroSeEstoqueInsuficiente()
		{
			var produto = _produtoFaker.Generate();
			var venda = _vendaFaker.Generate();
			venda.Itens[0].Produto = produto;
			venda.Itens[0].Quantidade = produto.Estoque + 1;

			_produtoRepositoryMock.ObterPorId(produto.Id).Returns(produto);

			var exception = Assert.Throws<InvalidOperationException>(() => _vendaService.RealizarVenda(venda));
			Assert.Equal("Estoque insuficiente", exception.Message);
		}

		[Fact]
		public void RealizarVenda_DeveCalcularTotalComDesconto()
		{
			var produto = _produtoFaker.Generate();
			var venda = _vendaFaker.Generate();
			venda.Itens[0].Produto = produto;
			venda.Itens[0].Quantidade = 2;
			venda.Desconto = 0.1m; 

			_produtoRepositoryMock.ObterPorId(produto.Id).Returns(produto);

			_vendaService.RealizarVenda(venda);

			var expectedTotal = (produto.Preco * venda.Itens[0].Quantidade) * (1 - venda.Desconto);
			Assert.Equal(expectedTotal, venda.Total);
		}
		[Fact]
		public void RealizarVenda_DeveRealizarVendaComVariosItens()
		{
			var produto1 = _produtoFaker.Generate();
			var produto2 = _produtoFaker.Generate();
			var venda = new Venda
			{
				Itens = new List<ItemVenda>
		{
			new ItemVenda { Produto = produto1, Quantidade = 1 },
			new ItemVenda { Produto = produto2, Quantidade = 2 }
		}
			};

			_produtoRepositoryMock.ObterPorId(produto1.Id).Returns(produto1);
			_produtoRepositoryMock.ObterPorId(produto2.Id).Returns(produto2);

			_vendaRepositoryMock.Adicionar(Arg.Any<Venda>()).Returns(Task.CompletedTask); 

			_vendaService.RealizarVenda(venda);

			_produtoRepositoryMock.Received(2).Atualizar(Arg.Any<Produto>());
			_vendaRepositoryMock.Received(1).Adicionar(Arg.Any<Venda>());
		}
	}

}
