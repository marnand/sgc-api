# Sistema de Gestão Centralizada (SGC)

![.NET 8](https://img.shields.io/badge/.NET-8.0-blueviolet)
![Build Status](https://img.shields.io/badge/build-passing-brightgreen)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📚 Sumário
- [Descrição](#-descrição)
- [Características Principais](#-características-principais)
- [Requisitos](#-requisitos)
- [Configuração](#-configuração)
- [Endpoints](#-endpoints)
- [Segurança](#-segurança)
- [Como Executar](#️-como-executar)
- [Desenvolvimento](#️-desenvolvimento)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Contribuição](#-contribuição)
- [Licença](#-licença)

---

## 📄 Descrição
O **SGC** é uma API RESTful desenvolvida em **.NET 8** que implementa um sistema de autenticação e gerenciamento de usuários utilizando **JWT (JSON Web Tokens)**.

## 🚀 Características Principais
- 🔒 Autenticação via JWT
- 👥 Gerenciamento de usuários
- 🔐 Criptografia segura de senhas usando PBKDF2
- 📚 Documentação via Swagger
- ❤️ Healthcheck endpoint
- 🌐 Suporte a CORS

## 🛠️ Requisitos
- 📦 .NET 8.0 SDK
- 🗄️ Banco de dados (configurável via connection string)
- 🖥️ Visual Studio 2022 ou VS Code

## ⚙️ Configuração

### 1. Configurações do `appsettings.json`
```json
{
  "ConnectionString": {
    "DefaultConnection": "sua_connection_string"
  },
  "TokenSettings": {
    "Secret": "sua_chave_secreta",
    "Issuer": "seu_issuer",
    "Audience": "sua_audience",
    "ExpirationInMinutes": 60
  }
}
```

### 2. Variáveis de Ambiente
- `DATABASE_URL`: (Opcional) Sobrescreve a connection string padrão.

## 🔗 Endpoints

### 🔐 Autenticação
- **POST** `/v1/api/user/login`
  - Permite login de usuários
  - Não requer autenticação

- **GET** `/v1/api/user`
  - Retorna informações do usuário atual
  - Requer autenticação via Bearer Token

### ❤️ Saúde da Aplicação
- **GET** `/health`
  - Endpoint para monitoramento da saúde da aplicação

### 📚 Documentação API
- **GET** `/swagger`
  - Interface Swagger disponível em ambiente de desenvolvimento

## 🔒 Segurança
- Implementação de PBKDF2 para hash de senhas
- Validação de tokens JWT
- HTTPS obrigatório em produção

## ▶️ Como Executar

1. Clone o repositório:
   ```bash
   git clone [url-do-repositorio]
   ```

2. Configure o `appsettings.json` com suas credenciais.

3. Execute as migrações do banco de dados (se aplicável).

4. Execute o projeto:
   ```bash
   dotnet run
   ```

## 🛠️ Desenvolvimento
- Utiliza o padrão **Repository**
- Injeção de dependência para serviços
- Tratamento de erros centralizado
- CORS configurado para desenvolvimento local

## 🧩 Estrutura do Projeto
```
├── Controllers/
│   └── UserController.cs
├── Domain/
│   ├── Entities/
│   ├── Interfaces/
│   └── Dtos/
├── Infra/
│   ├── Repositories/
│   └── Security/
├── Configs/
└── Program.cs
```

## 🤝 Contribuição

1. Faça um fork do projeto.
2. Crie uma branch para sua feature.
3. Commit suas mudanças.
4. Push para a branch.
5. Abra um Pull Request.

## 📜 Licença
Este projeto está licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.
