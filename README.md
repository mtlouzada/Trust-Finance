# Trust-Finance - API de Controle Financeiro

Trust-Finance é uma API REST construída com .NET 8 e ASP.NET Core, projetada para gerenciar finanças pessoais de forma segura e escalável. O sistema oferece recursos como cadastro de usuários, autenticação via JWT, controle de categorias e transações, além de documentação interativa com Swagger.

---

## Tecnologias Utilizadas

- .NET 8 + ASP.NET Core Web API
- Entity Framework Core 8
- SQL Server
- Docker + Docker Compose
- FluentValidation
- Hash de senhas com SHA256
- Swagger
- Autenticação com Token JWT
- **Testes de Unidade: xUnit + FluentAssertions**
- **EF Core InMemory para testes**

---

## Estrutura do Projeto

- `Models/` – Classes que representam as entidades do banco de dados (User, Category, etc).
- `ViewModels/` – Classes para validação e transporte seguro de dados entre a API e o cliente.
- `Controllers/` – Controladores responsáveis pelos endpoints da aplicação.
- `Extensions/` – Métodos de extensão, como tratamento de erros de `ModelState`.
- `Data/` – Contexto do Entity Framework (`TFDataContext`).
- `Services/` – Regras de negócio e serviços auxiliares (como geração de tokens).
- `Trust-Finance.Tests/` – Projeto de testes automatizados (xUnit + FluentAssertions).

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

## ✅ Testes de Unidade

O projeto possui um projeto de testes dedicado: **`Trust-Finance.Tests`**, com foco em validar regras de negócio na camada de **Services**, mantendo Controllers “finos” (HTTP only).

### Stack de testes

- **xUnit** (test runner)
- **FluentAssertions** (assertions mais expressivas)
- **EF Core InMemory** (banco em memória para isolamento e velocidade)
- **Fixtures** para criação do `TFDataContext`

### Cenários cobertos

- Cenários **positivos e negativos** para regras de negócio
- Exemplo: impedir cadastro de usuário com **e-mail duplicado**
- Garantia de consistência: ao falhar, **não persiste** dados indevidos

### Como rodar os testes

Na raiz do repositório:

```bash
dotnet test
```

Ou rodando apenas o projeto de testes:

```bash
dotnet test ./Trust-Finance.Tests/Trust-Finance.Tests.csproj
```

> Observação: os testes unitários não dependem do SQL Server, pois usam EF Core InMemory.

---

### 🐳 Docker e Docker Compose

O projeto utiliza Docker Compose para padronizar o ambiente de desenvolvimento, facilitando a execução do banco de dados SQL Server sem a necessidade de instalação local.

#### Benefícios do uso de Docker no projeto

- Ambiente reproduzível
- Banco de dados isolado em container
- Setup local mais simples
- Base preparada para testes automatizados e CI/CD

---

### Como rodar o projeto com Docker (recomendado)

#### Pré-requisitos

- Docker
- Docker Compose
- .NET SDK 8.x

---

## Como rodar o projeto

1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/tf-api.git
```

2. Suba o banco de dados com Docker Compose:

```bash
docker compose up -d
```

3. Crie o arquivo `appsettings.json` e configure a ConnectionString e JwtKey:

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

6. Acesse o Swagger:

```bash
http://localhost:5151/swagger
```
