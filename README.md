AiHackApi
Equipe de Desenvolvimento
RM99841 - Marcel Prado Soddano
RM551261 - Giovanni Sguizzardi
RM98057 - Nicolas E. Inohue
RM552302 - Samara Moreira
RM552293 - Vinicius Monteiro
Descrição do Projeto
A AiHackApi é uma API desenvolvida com ASP.NET Core para gerenciar consultas médicas e serviços de saúde, interagindo com um banco de dados Oracle. O sistema oferece operações CRUD (criação, leitura, atualização e exclusão) para diversas entidades, como:

Consultas Médicas
Pacientes
Médicos
Endereços
Contatos
Este projeto segue boas práticas de arquitetura e segurança, com foco em escalabilidade, manutenibilidade, e integridade dos dados.

Arquitetura
A arquitetura escolhida é monolítica, onde todos os componentes estão centralizados em um único projeto. Essa abordagem foi selecionada devido à simplicidade e fácil manutenção para este tipo de aplicação. O código está organizado em camadas, que facilitam a evolução do projeto:

Controllers: Manipulam as requisições HTTP e retornam respostas adequadas.
Services: Contêm a lógica de negócios.
Repositories: Gerenciam as operações com o banco de dados Oracle.
Models: Definem as entidades e o mapeamento de banco de dados.
Justificativa da Arquitetura
A abordagem monolítica foi escolhida por sua simplicidade em comparação a microservices. Para um sistema que ainda está em fases iniciais, a arquitetura monolítica oferece menor sobrecarga de gerenciamento e implantação, o que permite evoluir a solução de forma ágil e eficiente.

Padrões de Projeto Utilizados
Foram utilizados diversos padrões de projeto para garantir a eficiência e manutenibilidade da aplicação:

Singleton: Implementado no gerenciador de configurações, garantindo que apenas uma instância seja utilizada durante a execução.
Factory: Utilizado no ApplicationDbContext para criar e gerenciar as conexões com o banco de dados Oracle.
Injeção de Dependências: Implementada para desacoplar as diferentes camadas da aplicação, facilitando o desenvolvimento e os testes unitários.
Documentação Interativa
A API possui integração com o Swagger/OpenAPI para documentar e testar os endpoints diretamente no navegador. Essa interface é extremamente útil para desenvolvedores e testadores, pois oferece uma maneira interativa de explorar a API.

Acesse o Swagger em: Swagger UI.
Instruções para Executar a API
Pré-requisitos:
.NET SDK 8.0: Verifique a instalação do .NET com o comando:
:
dotnet --version
Banco de dados Oracle: Certifique-se de que o Oracle está configurado e rodando corretamente.
Passos:
Clone o Repositório:
:
git clone https://github.com/Nxtlevelbr/AiHackApi.git
:
cd AiHackApi/AiHackApi
Restaurar Pacotes e Construir o Projeto:
:
dotnet restore
dotnet build
Executar a Aplicação:
:
dotnet run
Fluxo de Inserção de Dados
Ordem sugerida para inserir os dados:
Inserir Especialidade
POST /api/especialidades: Registre as especialidades médicas antes de inserir médicos ou consultas.
Inserir Bairro
POST /api/bairros: Insira os bairros para referenciar nos endereços.
Inserir Endereço
POST /api/enderecos: Registre os endereços completos.
Inserir Contato
POST /api/contatos: Adicione informações de contato, como e-mail e telefone.
Inserir Paciente
POST /api/pacientes: Insira os pacientes relacionando-os com endereços e contatos.
Inserir Médico
POST /api/medicos: Registre os médicos com especialidades e contatos associados.
Inserir Consulta
POST /api/consultas: Registre consultas médicas associando médicos e pacientes.
Principais Endpoints para Testar
Consultas
GET /api/consultas: Lista todas as consultas.
GET /api/consultas/{id}: Detalha uma consulta específica.
POST /api/consultas: Cria uma nova consulta médica.
PUT /api/consultas/{id}: Atualiza uma consulta existente.
DELETE /api/consultas/{id}: Exclui uma consulta.
Testes:
GET /api/consultas: Certifique-se de que todas as consultas são retornadas.
POST /api/consultas: Verifique se novas consultas são criadas corretamente.
PUT /api/consultas/{id}: Verifique se as atualizações são aplicadas corretamente.
DELETE /api/consultas/{id}: Certifique-se de que consultas são removidas com sucesso.
Conclusão
Esta API foi construída com foco em boas práticas de arquitetura, padrões de projeto e documentação clara. A escolha da arquitetura monolítica e o uso de padrões como Singleton e Factory garantem escalabilidade e facilidade de manutenção.

