# Trust-Finance - API de Controle Financeiro

Esta � uma API REST desenvolvida com ASP.NET Core, voltada para controle financeiro pessoal. O projeto ainda est� em desenvolvimento, mas j� conta com funcionalidades como cadastro de categorias e usu�rios, autentica��o com JWT, documenta��o interativa via Swagger, e testes de unidade em andamento com MSTest.

---

## Tecnologias Utilizadas

- .NET 6
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- FluentValidation (via Data Annotations)
- SHA256 para hash de senhas
- Swagger
- Autentica��o com Token JWT
- MSTest (testes de unidade)

---

## Estrutura do Projeto

- `Models/` � Classes que representam as entidades do banco de dados (User, Category, etc).
- `ViewModels/` � Classes para valida��o e transporte seguro de dados entre a API e o cliente.
- `Controllers/` � Controladores respons�veis pelos endpoints da aplica��o.
- `Extensions/` � M�todos de extens�o, como tratamento de erros de `ModelState`.
- `Data/` � Contexto do Entity Framework (`TFDataContext`).
- `Services/` � Regras de neg�cio e servi�os auxiliares (como gera��o de tokens).
- `Tests/` � Projeto de testes de unidade utilizando MSTest.

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

### Autentica��o

- **POST /api/account/login** � Autentica um usu�rio e retorna um token JWT.
- **POST /api/account/register** � Registra um novo usu�rio com valida��o de dados.

> As senhas s�o armazenadas com hash SHA256. A autentica��o utiliza tokens JWT, protegendo rotas privadas com base em roles.

---

## Swagger

A documenta��o da API est� dispon�vel via Swagger, permitindo explorar e testar os endpoints diretamente pelo navegador, ap�s iniciar a aplica��o.

---

## Testes de Unidade

O projeto conta com testes de unidade em desenvolvimento utilizando MSTest. O objetivo � validar os fluxos principais, como cria��o de contas, autentica��o e manipula��o de categorias.

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
 - Cadastro de transa��es financeiras

 - Filtros por data e tipo de transa��o

 - Dashboard de resumo financeiro

 - Controle de permiss�es

 - Expans�o dos testes automatizados

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