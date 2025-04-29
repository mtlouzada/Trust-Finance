# Trust-Finance - API de Controle Financeiro

Esta é uma API REST desenvolvida com ASP.NET Core, voltada para controle financeiro pessoal. O projeto ainda está em desenvolvimento, mas já conta com funcionalidades básicas como cadastro de categorias e usuários, além de validação e tratamento de erros com ViewModels e Extensions.

---

## Tecnologias Utilizadas

- .NET 6
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- FluentValidation (via Data Annotations)
- SHA256 para hash de senhas
- Swagger (em breve)
  
---

## Estrutura do Projeto

- `Models/` – Classes que representam as entidades do banco de dados (User, Category, etc).
- `ViewModels/` – Classes para validação e transporte seguro de dados entre a API e o cliente.
- `Controllers/` – Controladores responsáveis pelos endpoints da aplicação.
- `Extensions/` – Métodos de extensão, como tratamento de erros de `ModelState`.
- `Data/` – Contexto do Entity Framework (`TFDataContext`).

---

## Funcionalidades já implementadas

### Categories

- **GET /api/categories** – Lista todas as categorias.
- **GET /api/categories/{id}** – Retorna uma categoria específica.
- **POST /api/categories** – Cria uma nova categoria.
- **PUT /api/categories/{id}** – Atualiza uma categoria existente.
- **DELETE /api/categories/{id}** – Remove uma categoria.

### Users

- **GET /api/users** – Lista todos os usuários.
- **GET /api/users/{id}** – Retorna um usuário específico.
- **POST /api/users** – Cria um novo usuário com validação de nome, email e senha.

> A senha é armazenada com hash SHA256 por enquanto. Futuramente será implementada autenticação com tokens JWT e validação segura com hasher mais robusto.

---

## Tratamento de Erros

Os erros de validação do `ModelState` são tratados com uma extension (`ModelStateExtension.cs`), que converte os erros em uma lista de strings para facilitar o retorno ao cliente.

As respostas da API seguem um padrão utilizando `ResultViewModel<T>`, facilitando o consumo no front-end:

```json
{
  "data": {
    "id": 1,
    "name": "Categoria Exemplo",
    "slug": "categoria-exemplo"
  },
  "errors": []
}
```

## Funcionalidades em desenvolvimento
 - Autenticação com Token JWT

 - Cadastro de transações financeiras

 - Filtros por data e tipo de transação

 - Dashboard de resumo financeiro

 - Controle de permissões

## Como rodar o projeto
1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/tf-api.git
```
2. Abra no Visual Studio ou VS Code.

3. Configure a ConnectionString no appsettings.json.

4. Aplique as migrations e crie o banco:

```bash
dotnet ef database update
```

5. Rode o projeto:
```bash
dotnet run
```
|