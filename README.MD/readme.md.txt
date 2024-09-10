# AiHackApi

## Equipe de Desenvolvimento
- **RM99841** - Marcel Prado Soddano
- **RM551261** - Giovanni Sguizzardi
- **RM98057** - Nicolas E. Inohue
- **RM552302** - Samara Moreira
- **RM552293** - Vinicius Monteiro

## Descrição do Projeto
A **AiHackApi** é uma API desenvolvida com **ASP.NET Core** para gerenciar consultas médicas e outros serviços de saúde. A API interage com um banco de dados **Oracle**, oferecendo uma interface RESTful que permite a criação, leitura, atualização e exclusão (CRUD) de consultas médicas. O projeto segue boas práticas de arquitetura e segurança, garantindo escalabilidade, manutenibilidade e integridade dos dados.

## Arquitetura
A API segue uma arquitetura **monolítica**, onde todos os componentes estão centralizados em um único projeto. O código está organizado em camadas, o que facilita a manutenção e evolução da aplicação. As camadas principais são:

- **Controllers**: Manipulam as requisições HTTP e retornam respostas.
- **Services**: Implementam a lógica de negócios.
- **Repositories**: Gerenciam as operações com o banco de dados Oracle.
- **Models**: Representam as entidades e o mapeamento para o banco de dados.

## Padrões de Projeto Utilizados

1. **Singleton**: Utilizado no gerenciador de configurações para garantir que apenas uma instância seja criada e utilizada durante a execução da aplicação.
2. **Factory**: Implementado no `ApplicationDbContext` para criar e gerenciar as conexões com o banco de dados Oracle.
3. **Injeção de Dependências**: Desacopla as camadas da aplicação, facilitando o desenvolvimento e os testes.

## Documentação Interativa
A API é documentada com **Swagger/OpenAPI**, permitindo que você explore e teste os endpoints diretamente no navegador. Isso facilita a integração com desenvolvedores e testadores, oferecendo uma interface amigável para interagir com a API.

## Instruções Detalhadas para Executar a API

### Requisitos
- **.NET SDK 8.0**: Certifique-se de que o SDK .NET 8.0 está instalado. Você pode verificar a versão instalada com o comando:
   ```bash
   dotnet --version

passo a passo:
Clone o Repositório: Primeiro, você deve clonar o repositório da API. Para isso, execute o seguinte comando no terminal ou prompt de comando:

git clone https://github.com/Nxtlevelbr/AiHackApi.git

Acesse o Diretório do Projeto: Navegue até o diretório onde o projeto foi clonado:

cd AiHackApi


Acessar a API pelo Navegador: Com a aplicação em execução, você pode acessar o Swagger para interagir com a documentação da API e testar os endpoints
. Abra seu navegador e acesse:


http://localhost:5000/index.html
	
Principais Endpoints para Testar
1. GET /api/consultas
Descrição: Lista todas as consultas cadastradas.
Teste: Verifique se as consultas são retornadas corretamente.
2. GET /api/consultas/{id}
Descrição: Retorna os detalhes de uma consulta específica.
Teste: Certifique-se de que o ID da consulta retorna as informações corretas.
3. POST /api/consultas
Descrição: Cria uma nova consulta médica.
Teste: Enviar uma nova consulta e verificar se ela foi registrada corretamente.
4. PUT /api/consultas/{id}
Descrição: Atualiza uma consulta existente.
Teste: Verifique se a atualização de dados ocorre corretamente.
5. DELETE /api/consultas/{id}
Descrição: Remove uma consulta com base no ID.
Teste: Verifique se a consulta foi removida com sucesso.



