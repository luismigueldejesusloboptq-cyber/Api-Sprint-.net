# Relatório de Projeto: API Lanchonete - Sprint Final

Sistema de gerenciamento de pedidos com catálogo de produtos e autenticação segura via JWT (JSON Web Token).

## Tecnologias Utilizadas
* **Back-end:** .NET 8 / Entity Framework Core
* **Banco de Dados:** MySQL
* **Front-end:** JavaScript (Fetch API), HTML5 e Bootstrap 5
* **Segurança:** Autenticação JWT

## Instruções para Execução

1. **Configuração do Banco de Dados:**
   - Aceda ao arquivo `appsettings.json`.
   - Insira as credenciais do seu servidor MySQL local na string de conexão `DefaultConnection`.
   
2. **Execução e Migrations:**
   - Ao iniciar o projeto no Visual Studio (F5), o sistema executará automaticamente o comando `db.Database.Migrate()`.
   - Este processo criará toda a estrutura de tabelas e relacionamentos (Migrations) no seu banco de dados de forma automática.

3. **Acesso ao Sistema:**
   - Execute o projeto e aceda à página `login.html` através do seu navegador.

## Credenciais de Teste
Para fins de avaliação, utilize o utilizador pré-configurado:
* **E-mail:** luis.lobo@ba.estudante.senai.br
* **Senha:** 12345

## Funcionalidades Implementadas
* **Migrations:** Versionamento completo do banco de dados, incluindo as tabelas de Utilizadores, Produtos, Categorias, Fornecedores, Pedidos e Itens de Pedido.
* **Guarda de Rota:** A página `vitrine.html` possui um script de validação que redireciona utilizadores não autenticados de volta para a tela de login.
* **Carrinho de Compras:** Lógica de gestão de itens (adição e remoção) e cálculo de total em tempo real via JavaScript.
* **Finalização de Pedido:** Processamento e envio dos dados do carrinho para a API, validando o token JWT e vinculando o registo ao cliente correspondente.

## Detalhes Técnicos
* **Autorização Bearer:** Endpoints sensíveis da API protegidos com o atributo `[Authorize]`.
* **Persistência de Sessão:** Utilização de `localStorage` para armazenamento do token e manutenção do estado de autenticação.
* **Integridade de Dados:** Banco de dados estruturado com Chaves Estrangeiras (FKs) e suporte a operações de eliminação em cascata (Cascade Delete).
