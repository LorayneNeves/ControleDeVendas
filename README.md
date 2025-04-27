
ControleDeVendas

📋 Sobre o Projeto

O ControleDeVendas é uma API desenvolvida em .NET com o objetivo de gerenciar produtos e vendas de maneira simples e eficiente. O projeto simula um sistema de controle de estoque e registro de vendas utilizando uma estrutura em memória.

🛠️ Funcionalidades

- Gerenciamento de Produtos:
  - Buscar produto por ID.
  - Listar todos os produtos disponíveis.
  - Atualizar informações de estoque.

- Gerenciamento de Vendas:
  - Realizar venda de produtos.
  - Verificar estoque disponível antes de confirmar a venda.
  - Atualizar o estoque automaticamente após a venda.
  - Calcular o valor total da venda, aplicando desconto, se informado.

- Validações:
  - Impede vendas com estoque insuficiente.
  - Lança exceções claras em casos de erro.

🧱 Estrutura do Projeto

- Application/Services  
  Camada responsável pela lógica de negócio (ProdutoService e VendaService).

- Controllers  
  Camada que recebe as requisições HTTP (VendaController).

- Data/Repository  
  Repositórios que armazenam dados em memória (ProdutoRepository, VendaRepository).

- Domain/Entities  
  Definição das entidades principais do domínio (Produto, Venda, ItemVenda).

- Domain/Interfaces  
  Interfaces dos repositórios, garantindo separação e flexibilidade no código.

- Testes  
  Testes unitários para validar o comportamento dos serviços (ProdutoServiceTests, VendaServiceTests).

🧪 Testes Automatizados

O projeto conta com uma bateria de testes unitários para assegurar:
- A busca correta de produtos.
- A validação do estoque antes da venda.
- O cálculo correto do total da venda com descontos.
- O correto registro de vendas com múltiplos itens.

As ferramentas utilizadas para os testes incluem XUnit, FluentAssertions, NSubstitute e Bogus.

🧠 Conceitos Trabalhados

- Organização em camadas (separação de responsabilidades).
- Uso de interfaces para abstração de dependências.
- Programação assíncrona (async/await).
- Testes automatizados abrangendo diferentes cenários de negócio.
