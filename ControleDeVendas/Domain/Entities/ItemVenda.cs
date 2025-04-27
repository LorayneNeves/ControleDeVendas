namespace ControleDeVendas.Domain.Entities
{
    public class ItemVenda
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Total => Produto.Preco * Quantidade;
    }
}
