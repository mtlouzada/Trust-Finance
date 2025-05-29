# Trust-Finance - API de Controle Financeiro

Trust-Finance é uma API REST construída com .NET 8 e ASP.NET Core, projetada para gerenciar finanças pessoais de forma segura e escalável. O sistema oferece recursos como cadastro de usuários, autenticação via JWT, controle de categorias e transações, além de documentação interativa com Swagger.

---

## Tecnologias Utilizadas

- .NET 8 + ASP.NET Core Web API
- Entity Framework Core + SQL Server
- FluentValidation
- Hash de senhas com SHA256
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

## Funcionalidades implementadas

### Categorias

- Lista todas as categorias
- Busca por ID
- Cria nova categoria
- Atualiza categoria
- Remove categoria

### Usuários

- Lista todos os usuários.
- Busca por ID

### Autenticação

- Registro de novos usuário com validação
- Login com retorno de token JWT
- Proteção de rotas com [Authorize] por Role

### Transações

- Criar transações vinculadas ao usuário autenticado
- Listar, editar e excluir transações

> As senhas são protegidas com hash SHA256. A autenticação usa JWT, garantindo acesso apenas a usuários autorizados.

### Swagger

A documentação da API está disponível via Swagger, permitindo explorar e testar os endpoints diretamente pelo navegador, após iniciar a aplicação.

---

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