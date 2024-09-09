using AiHackApi.Data;
using AiHackApi.Repositories;
using AiHackApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

// Configurar a string de conexão
string? connectionString = builder.Configuration.GetConnectionString("OracleConnection");

// Verificar se a string de conexão não é nula ou vazia
if (string.IsNullOrEmpty(connectionString))
{
    // Lançar uma exceção se a string de conexão não for encontrada no appsettings.json
    throw new InvalidOperationException("A string de conexão 'OracleConnection' não foi encontrada no appsettings.json.");
}

// Registrar o ApplicationDbContext
//configura o DbContext para usar Oracle com a string de conexão configurada
//tempo limite de comando de 60 segundos para evitar travamentos em consultas longas
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseOracle(connectionString, oracleOptions =>
    {
        oracleOptions.CommandTimeout(60); // Timeout de comando opcional (60 segundos)
    });
});

// Registrar Controladores
//todos os controladores (endpoints) sejam mapeados e usados corretamente
builder.Services.AddControllers();

// Configurar Swagger (ativo em todas as fases)
//.EnableAnnotations() permite que anotações sejam usadas para documentar rotas e métodos
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AiHackApi",
        Version = "v1",
        Description = "API para gerenciamento de Consultas Médicas da AiHack"
    });
    c.EnableAnnotations(); // Habilitar anotações no Swagger
});

// Registrar repositórios e serviços
// Aqui estao registrando todas as interfaces e implementações para repositórios e serviços
// Utilizando o AddScoped, uma nova instância de cada serviço/repositório será criada por requisição HTTP
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();

builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IMedicoService, MedicoService>();

builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IPacienteService, PacienteService>();

builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();

builder.Services.AddScoped<IBairroRepository, BairroRepository>();
builder.Services.AddScoped<IBairroService, BairroService>();

builder.Services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
builder.Services.AddScoped<IEspecialidadeService, EspecialidadeService>();

builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();

var app = builder.Build();

// Middleware global para tratamento de exceções
// Middleware para capturar erros globais e retornar mensagens personalizadas
// Pode ser configurado para produzir uma resposta de erro genérica em caso de exceções não tratadas
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError; // Código de erro 500
        context.Response.ContentType = "application/json"; // O retorno será em formato JSON

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            await context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                Details = contextFeature?.Error.Message ?? "Erro desconhecido" // Verificação de nulidade
            }.ToString() ?? ""); // Garante que o WriteAsync não receba null

        }
    });
});

//Configurar Swagger para UI e documentação
// Swagger gerará a interface visual da API, acessível na raiz do site (/)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AiHackApi v1");
    c.RoutePrefix = string.Empty; // Faz com que o Swagger seja acessível na raiz
});

// Middleware de autorização
// Autorização garante que apenas usuários autenticados acessem endpoints protegidos
app.UseAuthorization();

// Mapear controladores
// Mapear todas as rotas definidas nos controladores
app.MapControllers();

// Executar a aplicação
// Inicializa a aplicação e começa a escutar requisições HTTP
app.Run();
