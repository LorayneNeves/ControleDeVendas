    
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeVendas.Domain.Entities
{
    public class Produto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
    }

}


