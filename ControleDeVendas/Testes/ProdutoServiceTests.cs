using Xunit;
using FluentAssertions;
using NSubstitute;
using Bogus;
using System;
using System.Threading.Tasks;
using ControleDeVendas.Application.Services;
using ControleDeVendas.Domain.Entities;
using ControleDeVendas.Domain.Interface;

namespace ControleDeVendas.Testes.ProdutoServiceTests
{
	public class ProdutoServiceTests
	{
		private readonly IProdutoRepository _produtoRepositoryMock;
		private readonly ProdutoService _produtoService;
		private readonly Faker<Produto> _produtoFaker;

		public ProdutoServiceTests()
		{
			_produtoRepositoryMock = Substitute.For<IProdutoRepository>();
			_produtoService = new ProdutoService(_produtoRepositoryMock);

			_produtoFaker = new Faker<Produto>()
				.RuleFor(p => p.Id, f => Guid.NewGuid())
				.RuleFor(p => p.Nome, f => f.Commerce.ProductName())
				.RuleFor(p => p.Preco, f => f.Random.Decimal(1, 1000))
				.RuleFor(p => p.Estoque, f => f.Random.Int(0, 100));
		}

		[Fact]
		public async Task BuscarProdutoPorId_DeveRetornarProduto_QuandoIdValido()
		{
			var produto = _produtoFaker.Generate();
			_produtoRepositoryMock.BuscarPorIdAsync(produto.Id).Returns(Task.FromResult(produto));

			var resultado = await _produtoService.BuscarPorIdAsync(produto.Id);

			resultado.Should().NotBeNull();
			resultado.Id.Should().Be(produto.Id);
			resultado.Nome.Should().Be(produto.Nome);
			resultado.Preco.Should().BeGreaterThan(0);
		}

		[Fact]
		public async Task BuscarProdutoPorId_DeveLancarExcecao_QuandoProdutoNaoEncontrado()
		{
			var idInvalido = Guid.NewGuid();
			_produtoRepositoryMock.BuscarPorIdAsync(idInvalido).Returns(Task.FromResult<Produto?>(null));

			Func<Task> act = async () => await _produtoService.BuscarPorIdAsync(idInvalido);

			await act.Should().ThrowAsync<InvalidOperationException>()
				.WithMessage("Produto não encontrado");
		}

		[Fact]
		public async Task ListarProdutos_DeveRetornarListaDeProdutos()
		{
			var produtos = _produtoFaker.Generate(3);
			_produtoRepositoryMock.ObterTodosAsync().Returns(Task.FromResult<IEnumerable<Produto>>(produtos));

			var resultado = await _produtoService.ListarProdutosAsync();

			resultado.Should().HaveCount(3);
			resultado.Should().Contain(p => p.Preco > 0);
			resultado.Should().OnlyContain(p => !string.IsNullOrEmpty(p.Nome));
		}
	}
}
