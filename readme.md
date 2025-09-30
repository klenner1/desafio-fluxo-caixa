# 🚀 Sistema de Fluxo de Caixa

## 🎯 Objetivo do Projeto

Este repositório contém uma solução de backend desenvolvida para um desafio de Arquitetura de Software. O objetivo é implementar um sistema de **Controle de Fluxo de Caixa** que utiliza padrões de arquitetura modernos para desacoplamento e escalabilidade.

A solução implementa o padrão CQRS, onde as operações de escrita (cadastro de lançamentos) e as operações de leitura/relatório (consolidação de saldo) são tratadas por serviços e modelos de dados separados, com **consistência eventual** garantida por mensageria.

## 🛠️ Stack de Tecnologia

| Categoria | Tecnologia | Observações |
| :--- | :--- | :--- |
| **Framework** | **.NET 9** | Base para todas as aplicações. |
| **Banco de Dados** | **PostgreSQL** | Foi usado como Banco de dados para o Modelo de Escrita e para o modelo de Leitura. |
| **Mensageria** | **RabbitMQ** | Message Broker para comunicação assíncrona entre o serviço de escrita e o worker de consolidação. |
| **Contêineres** | **Docker / Docker Compose** | Orquestração e execução local de todos os serviços. |
| **Testes** | **xUnit, Moq, FluentAssertions, Bogus** | Ferramentas para testes unitários e de integração. |
| **Validações** | **FluentValidation** | Validação de requisições e entidades. |
| **Geração PRD** | **QuestPDF** |  Biblioteca open source para geração de PDF. |

## 📐 Arquitetura da Solução (CQRS e Consistência Eventual)

A solução é composta por três componentes principais, que se comunicam via RabbitMQ para garantir o desacoplamento:

### 1\. `fluxo-caixa-api` (Command Side)

  * **Função:** Recebe as requisições HTTP para cadastrar novos lançamentos de **Entrada** ou **Saída** (Comandos).
  * **Ação:** Salva o lançamento no Banco de Escrita (PostgreSQL) e, em seguida, **publica um evento** no RabbitMQ.
  * **Porta Local:** `7550`

### 2\. `consolidado-worker` (Projetor/Event Handler)

  * **Função:** Atua como o **Processador de Eventos**. Escuta os eventos de lançamento publicados pelo `fluxo-caixa-api` no RabbitMQ.
  * **Ação:** Aplica a lógica de **Projeção**, atualizando de forma assíncrona o **saldo consolidado** no Banco de Leitura.

### 3\. `consolidado-api` (Query Side)

  * **Função:** Recebe as requisições HTTP para gerar o **Relatório de Consolidação de Saldo** (Consultas).
  * **Ação:** Acessa **apenas o Banco de Leitura** para retornar o saldo consolidado, garantindo respostas rápidas e otimizadas.
  * **Porta Local:** `7551`

-----

## 💻 Configuração e Execução Local

### Pré-requisitos

  * [Docker](https://www.docker.com/products/docker-desktop)
  * [Git](https://git-scm.com/downloads)
  * [VS Code (Opcional)](https://code.visualstudio.com/) com a extensão [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)

### 1\. Clonar o Repositório

```bash
git clone https://github.com/klenner1/desafio-fluxo-caixa.git
cd desafio-fluxo-caixa
```

### 2\. Executar as Aplicações

O arquivo `compose.yml` inicia todos os serviços necessários (PostgreSQL, RabbitMQ, APIs e Worker).

```bash
docker compose up -d --build
```

## ⚙️ Uso da Aplicação (Testando o Fluxo)

Após a execução, você pode interagir com as APIs das seguintes formas:

### A. Via Arquivo `requests.http`

O arquivo `requests.http` na raiz do projeto contém exemplos formatados para o **REST Client** do VS Code, permitindo testar facilmente os fluxos de escrita e leitura.

### B. Via Swagger UI

Você pode acessar os painéis do Swagger para testar as rotas manualmente:

| Serviço | URL do Swagger |
| :--- | :--- |
| **fluxo-caixa-api** | [http://localhost:7550/swagger/index.html](https://www.google.com/search?q=http://localhost:7550/swagger/index.html) |
| **consolidado-api** | [http://localhost:7551/swagger/index.html](https://www.google.com/search?q=http://localhost:7551/swagger/index.html) |

#### Fluxo de Teste Sugerido:

1.  **Escrita (fluxo-caixa-api):** Use a rota POST para criar lançamentos de `Entrada` e `Saída`.
2.  **Verificação Assíncrona (consolidado-worker):** Aguarde alguns segundos (o tempo que o worker leva para consumir a mensagem do RabbitMQ e processar).
3.  **Leitura (consolidado-api):** Use a rota GET para obter o relatório de consolidação de saldo e verificar se os lançamentos foram aplicados.

-----

## 📈 Potenciais de Melhorias 

  * **Substituição do RabbitMQ:** Mudar para uma plataforma de **Event Streaming** como o Kafka para dar suporte completo ao padrão **Event Sourcing**, permitindo a reconstrução de projeções.
  * **Banco de Dados de Leitura:** Utilizar um banco de dados NoSQL (ex: MongoDB) no modelo de Leitura para otimizar ainda mais o desempenho das consultas.
  * **Adição de Caching:** Implementar camadas de caching (ex: Redis) no modelo de Leitura para consultas frequentes.