# Trust-Finance - API de Controle Financeiro

Esta é uma API REST desenvolvida com ASP.NET Core, voltada para controle financeiro pessoal. O projeto ainda está em desenvolvimento, mas já conta com funcionalidades como cadastro de categorias e usuários, autenticação com JWT, documentação interativa via Swagger, e testes de unidade em andamento com MSTest.

---

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- FluentValidation
- SHA256 para hash de senhas
- Swagger
- Autenticação com Token JWT

---

## Estrutura do Projeto

- `Models/` – Classes que representam as entidades do banco de dados (User, Category, etc).
- `ViewModels/` – Classes para validação e transporte seguro de dados entre a API e o cliente.
- `Controllers/` – Controladores responsáveis pelos endpoints da aplicação.
- `Extensions/` – Métodos de extensão, como tratamento de erros de `ModelState`.
- `Data/` – Contexto do Entity Framework (`TFDataContext`).
- `Services/` – Regras de negócio e serviços auxiliares (como geração de tokens).

---

## Funcionalidades já implementadas

### Categories

– Lista todas as categorias.
– Retorna uma categoria específica.
– Cria uma nova categoria.
- Atualiza uma categoria existente.
– Remove uma categoria.

### Users

- Lista todos os usuários.
- Retorna um usuário específico.

### Autenticação

– Registra um novo usuário com validação de dados.
– Autentica um usuário e retorna um token JWT.

> As senhas são armazenadas com hash SHA256. A autenticação utiliza tokens JWT, protegendo rotas privadas com base em roles.

---

## Swagger

A documentação da API está disponível via Swagger, permitindo explorar e testar os endpoints diretamente pelo navegador, após iniciar a aplicação.

---

## Funcionalidades em desenvolvimento
 - Cadastro de transações financeiras

 - Filtros por data e tipo de transação

 - Dashboard de resumo financeiro

 - Controle de permissões

 - Expansão dos testes automatizados

## Como rodar o projeto
1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/tf-api.git
```
2. Abra no Visual Studio.

3. Crie o arquivo appsettings.json e configure a ConnectionString e JwtKey.

```json
{
    "ConnectionStrings": {
        "DefaultConnection": ""
    },
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "JwtKey": "",
    "AllowedHosts": "*"
}
```

4. Aplique as migrations e crie o banco:

```bash
dotnet ef database update
```

5. Rode o projeto:
```bash
dotnet run
```
|
