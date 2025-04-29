# Trust-Finance - API de Controle Financeiro

Esta � uma API REST desenvolvida com ASP.NET Core, voltada para controle financeiro pessoal. O projeto ainda est� em desenvolvimento, mas j� conta com funcionalidades b�sicas como cadastro de categorias e usu�rios, al�m de valida��o e tratamento de erros com ViewModels e Extensions.

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

- `Models/` � Classes que representam as entidades do banco de dados (User, Category, etc).
- `ViewModels/` � Classes para valida��o e transporte seguro de dados entre a API e o cliente.
- `Controllers/` � Controladores respons�veis pelos endpoints da aplica��o.
- `Extensions/` � M�todos de extens�o, como tratamento de erros de `ModelState`.
- `Data/` � Contexto do Entity Framework (`TFDataContext`).

---

## Funcionalidades j� implementadas

### Categories

- **GET /api/categories** � Lista todas as categorias.
- **GET /api/categories/{id}** � Retorna uma categoria espec�fica.
- **POST /api/categories** � Cria uma nova categoria.
- **PUT /api/categories/{id}** � Atualiza uma categoria existente.
- **DELETE /api/categories/{id}** � Remove uma categoria.

### Users

- **GET /api/users** � Lista todos os usu�rios.
- **GET /api/users/{id}** � Retorna um usu�rio espec�fico.
- **POST /api/users** � Cria um novo usu�rio com valida��o de nome, email e senha.

> A senha � armazenada com hash SHA256 por enquanto. Futuramente ser� implementada autentica��o com tokens JWT e valida��o segura com hasher mais robusto.

---

## Tratamento de Erros

Os erros de valida��o do `ModelState` s�o tratados com uma extension (`ModelStateExtension.cs`), que converte os erros em uma lista de strings para facilitar o retorno ao cliente.

As respostas da API seguem um padr�o utilizando `ResultViewModel<T>`, facilitando o consumo no front-end:

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
 - Autentica��o com Token JWT

 - Cadastro de transa��es financeiras

 - Filtros por data e tipo de transa��o

 - Dashboard de resumo financeiro

 - Controle de permiss�es

## Como rodar o projeto
1. Clone o reposit�rio:

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