**1. Descrição do Projeto**

Aplicação web fullstack para gestão de lanchonete, com backend em C# e frontend em Angular, hospedados em nuvem.


**2. Tecnologias Utilizadas**

Linguagem (back): C# com .NET 10

Framework (back): ASP.NET Core Web API

Banco de Dados: MySQL com Entity Framework Core (Pomelo)

Segurança: JWT para autenticação, BCrypt para hash de senhas

Documentação de API: Swagger / OpenAPI

Linguagem (front): TypeScript

Framework (front): Angular 19

Estilização: Bootstrap 5

Hospedagem: Railway (back + banco) e Vercel (front)


**3. Instruções de Execução Local**

Instalar .NET 10 SDK, Node.js 18+, MySQL e Angular CLI

Configurar a connection string no appsettings.json com os dados do MySQL local

Na pasta api/LanchoneteAPI/LanchoneteAPI, rodar dotnet run — as migrations e dados iniciais são aplicados automaticamente

Na pasta web/Lanchonete, rodar npm install e depois ng serve

Acessar http://localhost:4200

Credenciais padrão: Admin — admin@lanchonete.com / Admin@123 | Funcionário — funcionario@lanchonete.com / Func@123


**4. Endpoints da API**
   
URL base: https://projetofinalbackend-production.up.railway.app/api

POST - /auth/login - Login e retorno do token JWT

GET - /usuarios - Lista funcionários (admin)

POST - /usuarios - Cadastra funcionário (Admin)

PUT - /usuarios/{id} - Atualiza funcionário (Admin)

GET - /categorias - Lista categorias

POST - /categorias - Cria categoria (Admin)

PUT - /categorias/{id} - Atualiza categoria (Admin)

DELETE - /categorias/{id} - Remove categoria (Admin)

GET - /produtos - Lista produtos

POST - /produtos - Cadastra produto (Admin)

PUT - /produtos/{id} - Atualiza produto (Admin)

PATCH - /produtos/{id}/estoque - Atualiza estoque

DELETE - /produtos/{id} - Remove produto (Admin)

GET - /pedidos - Lista pedidos com filtros

POST - /pedidos - Cria novo pedido

PATCH - /pedidos/{id}/status - Atualiza status do pedido

DELETE - /pedidos/{id} - Cancela pedido

GET - /cupons - Lista cupons (Admin)

POST - /cupons - Cria cupom (Admin)

POST - /cupons/validar - Valida cupom no pedido

DELETE - /cupons/{id} - Inativa cupom (Admin)

GET - /dashboard - Resumo do dia (Admin)

POST - /relatorios/vendas - Relatório por período (Admin)


**5. Arquitetura**

O backend segue arquitetura em camadas: Controllers (recebem as requisições) → Services (regras de negócio) → Repositories (acesso ao banco) → Models (entidades) → DTOs (objetos de entrada/saída).


**6. Hospedagem em Produção**

Frontend: Vercel — https://projeto-final-back-end-ruddy.vercel.app

Backend: Railway — https://projetofinalbackend-production.up.railway.app

Banco de dados: Railway (MySQL) — acesso interno
