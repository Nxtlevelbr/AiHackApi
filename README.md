AiHackApi

Equipe de Desenvolvimento:

RM99841 - Marcel Prado Soddano
RM551261 - Giovanni Sguizzardi
RM98057 - Nicolas E. Inohue
RM552302 - Samara Moreira
RM552293 - Vinicius Monteiro

Descrição do Projeto

AiHackApi é uma API RESTful construída com ASP.NET Core para gerenciar consultas médicas e outros serviços de saúde.
A aplicação interage diretamente com um banco de dados Oracle e oferece operações CRUD para diversas entidades 
relacionadas ao gerenciamento de saúde, como:

Consultas Médicas
Pacientes
Médicos
Endereços
Contatos

O projeto foi desenvolvido com foco em boas práticas de arquitetura e segurança, garantindo escalabilidade,
facilidade de manutenção e integridade dos dados.

Arquitetura
A arquitetura escolhida é monolítica em camadas. Optamos por essa abordagem por sua simplicidade e eficiência
para o contexto deste projeto. A estrutura monolítica centraliza todos os componentes em um único projeto, 
o que facilita o gerenciamento, a manutenção e a implantação.

Camadas da Aplicação
Controllers

Responsáveis por receber as requisições HTTP, interagir com os serviços e retornar as respostas apropriadas.
Aqui, as validações básicas são feitas, e a chamada para as operações principais é delegada à camada de serviços.
Services

Contém a lógica de negócios, lidando com as regras que regem o comportamento da aplicação.
A camada de serviços interage com os repositórios e aplica as validações ou transformações de dados antes de responder
ao controller.
Repositories

Gerencia o acesso ao banco de dados, utilizando Entity Framework Core e Oracle.
Os repositórios encapsulam as operações de leitura e escrita, mantendo a lógica de persistência desacoplada das demais
camadas.
Models

Representam as entidades do sistema (como Paciente, Consulta, Médico) e são responsáveis pelo mapeamento das tabelas 
no banco de dados.
DTOs (Data Transfer Objects)

Definem a estrutura de dados que é enviada ou recebida pelas requisições HTTP. Os DTOs garantem que apenas os dados
necessários sejam expostos, promovendo uma maior segurança e eficiência.

Justificativa da Arquitetura

A escolha de uma arquitetura monolítica foi feita por ser a abordagem mais simples e direta para o escopo atual do
projeto. Em uma fase inicial, essa abordagem permite uma menor sobrecarga na administração da solução, facilitando
o desenvolvimento ágil e a implementação de mudanças. Futuramente, caso o projeto cresça em complexidade,
a migração para uma arquitetura baseada em microservices pode ser considerada.

Pontos principais da arquitetura monolítica:

Simplicidade: Facilita o entendimento e a manutenção, sem a necessidade de lidar com a complexidade de comunicação 
entre microserviços.
Desempenho: Em um sistema monolítico, todas as partes da aplicação estão integradas, permitindo que a comunicação
interna entre camadas seja eficiente.
Facilidade de Deploy: Toda a aplicação é implantada de uma só vez, facilitando a administração da infraestrutura.
Padrões de Projeto Utilizados
Para garantir manutenibilidade, modularidade e desempenho, os seguintes design patterns foram aplicados:

Injeção de Dependência (Dependency Injection): Utilizada para desacoplar as camadas da aplicação. Isso facilita a 
substituição de implementações, tornando o sistema mais flexível e testável.
Repository Pattern: Cada entidade possui um repositório específico que abstrai as operações com o banco de dados.
Isso permite que a lógica de acesso a dados seja isolada e reutilizável.
Factory Pattern: Utilizado para instanciar o ApplicationDbContext de maneira organizada e eficiente, facilitando a 
criação de objetos relacionados ao banco de dados Oracle.
Singleton: Aplicado ao gerenciador de configurações para garantir que uma única instância seja utilizada durante 
toda a execução da aplicação.
Documentação Interativa
A API é totalmente documentada e integrada com Swagger/OpenAPI, o que permite a exploração interativa dos endpoints
diretamente no navegador. O Swagger facilita o entendimento das operações disponíveis, parâmetros esperados e tipos
de retorno, além de ser uma excelente ferramenta para testes.

Acesse a interface do Swagger em: http://localhost:{porta}/swagger.
Instruções para Executar a API
Pré-requisitos:
.NET SDK 8.0: Verifique se o SDK está instalado com o comando:


dotnet --version
Banco de Dados Oracle: O Oracle Database deve estar configurado e em execução.

Passos para executar a aplicação:
Clone o Repositório:


git clone https://github.com/Nxtlevelbr/AiHackApi.git
cd AiHackApi/AiHackApi
Restaurar Pacotes e Compilar o Projeto:


dotnet restore
dotnet build
Executar a Aplicação:


dotnet run
Fluxo de Inserção de Dados
Para garantir a consistência dos dados, sugerimos que as inserções sigam a ordem abaixo:

Inserir Especialidade:

Endpoint: POST /api/especialidades
Descrição: Registre as especialidades médicas antes de inserir médicos ou consultas.
Inserir Bairro:

Endpoint: POST /api/bairros
Descrição: Insira os bairros para referenciar nos endereços.
Inserir Endereço:

Endpoint: POST /api/enderecos
Descrição: Registre os endereços completos.
Inserir Contato:

Endpoint: POST /api/contatos
Descrição: Adicione informações de contato, como e-mail e telefone.
Inserir Paciente:

Endpoint: POST /api/pacientes
Descrição: Insira os pacientes relacionando-os com endereços e contatos.
Inserir Médico:

Endpoint: POST /api/medicos
Descrição: Registre os médicos com especialidades e contatos associados.
Inserir Consulta:

Endpoint: POST /api/consultas
Descrição: Registre consultas médicas associando médicos e pacientes.
Principais Endpoints para Testar
Consultas:
GET /api/consultas: Lista todas as consultas.
GET /api/consultas/{id}: Detalha uma consulta específica.
POST /api/consultas: Cria uma nova consulta médica.
PUT /api/consultas/{id}: Atualiza uma consulta existente.
DELETE /api/consultas/{id}: Exclui uma consulta.

O projeto AiHackApi foi desenvolvido seguindo as melhores práticas de desenvolvimento de software,
com uma estrutura modular e organizada. A escolha por uma arquitetura monolítica e a utilização de padrões como
Singleton, Factory e Repository garantem a escalabilidade e a facilidade de manutenção da aplicação. 
A documentação por meio do Swagger também facilita o entendimento e a experimentação dos endpoints.

Equipe de Desenvolvimento:

RM99841 - Marcel Prado Soddano
RM551261 - Giovanni Sguizzardi
RM98057 - Nicolas E. Inohue
RM552302 - Samara Moreira
RM552293 - Vinicius Monteiro
