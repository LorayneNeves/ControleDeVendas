using ControleDeVendas.Domain.Entities;

namespace ControleDeVendas.Domain.Entities
{
    public class Venda
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<ItemVenda> Itens { get; set; } = new();
        public decimal Desconto { get; set; }
        public decimal Total => Itens.Sum(i => i.Total) * (1 - Desconto);
    }
}


