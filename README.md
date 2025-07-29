# 📘 Blog API

API para gerenciamento de posts de blog com autenticação JWT, SignalR para notificações em tempo real via WebSocket e Swagger para documentação.

---

## 🚀 Tecnologias Utilizadas

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **MySQL**
- **SignalR**
- **JWT (Json Web Token)**
- **Swagger**
- **Docker & Docker Compose**

---

## 🧩 Funcionalidades

- CRUD de Posts
- Autenticação via JWT
- Notificações em tempo real com SignalR
- Swagger UI para teste das rotas

---

## ⚙️ Como executar o projeto

### Pré-requisitos

- Docker instalado
- Docker Compose instalado

---

### 🔧 Passo a passo para subir a aplicação

1. Clone este repositório:

   ```bash
   git clone https://github.com/zWalterli/Blog.API
   cd Blog.API
   ```

2. Suba a aplicação com Docker:

   ```bash
   docker-compose up -d
   ```

3. Acesse a documentação da API no navegador:
   ```
   http://localhost:9000/swagger
   ```

---

## 🔐 Endpoints protegidos

A autenticação é feita via **JWT Bearer Token**. Após o login, inclua o token no Swagger clicando em **Authorize**.

---

## 📡 WebSocket com SignalR

Hub disponível na rota:

```
http://localhost:9000/hub/PostNotification
```

O cliente pode se conectar e ouvir o evento `"NewPost"` para receber notificações quando um novo post for criado.

---

## 🧪 Migrations automáticas

As migrations do banco são aplicadas automaticamente na inicialização da API, com tentativas de conexão em loop até o MySQL estar disponível.

---

## 📄 Licença

Este projeto está licenciado sob a MIT License.
