# 💸 GranaFlowAPI

API para gerenciamento financeiro pessoal, desenvolvida em **.NET / C#**, com organização em camadas seguindo os princípios de **Clean Architecture**.

O projeto foi estruturado para separar responsabilidades entre domínio, aplicação, infraestrutura e API, facilitando manutenção, escalabilidade e evolução da regra de negócio.

---

## 🚀 Sobre o projeto

O **GranaFlowAPI** é uma API voltada para controle financeiro, permitindo centralizar regras de negócio e expor endpoints para operações como cadastro, autenticação e gerenciamento de dados financeiros.

A arquitetura do projeto foi dividida em múltiplas camadas para manter o código limpo, desacoplado e mais fácil de testar.

---

## 🧱 Arquitetura
O repositório está organizado em quatro projetos principais: `GranaFlow.API`, `GranaFlow.Application`, `GranaFlow.Domain` e `GranaFlow.Infrastructure`.

### `GranaFlow.API`
Camada de apresentação da aplicação.

Responsável por:
- Exposição dos endpoints HTTP
- Controllers
- Middlewares
- Extensions
- Configurações do `Program.cs`
- Arquivos de configuração (`appsettings.json`)

### `GranaFlow.Application`
Camada de aplicação e orquestração dos casos de uso.

Responsável por:
- Serviços da aplicação
- DTOs
- Autenticação
- Utilitários
- Regras de fluxo entre API, domínio e infraestrutura

### `GranaFlow.Domain`
Camada central da regra de negócio.

Responsável por:
- Entidades
- Enums
- Interfaces de repositórios
- Contratos do domínio

### `GranaFlow.Infrastructure`
Camada de acesso externo e implementação técnica.

Responsável por:
- Persistência de dados
- Repositórios concretos
- Serviços de infraestrutura
- Configuração de dados

---

## 📁 Estrutura do projeto

```bash
GranaFlowAPI
├── GranaFlow.API
│   ├── Constants
│   ├── Controllers
│   ├── Extensions
│   ├── Middlewares
│   └── Program.cs
│
├── GranaFlow.Application
│   ├── Auth
│   ├── Dtos
│   ├── Services
│   └── Utils
│
├── GranaFlow.Domain
│   ├── Entities
│   ├── Enums
│   └── Interfaces
│
├── GranaFlow.Infrastructure
│   ├── Data
│   ├── Repositories
│   └── Service
│
└── GranaFlow.slnx
