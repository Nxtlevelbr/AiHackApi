using AiHackApi.Data;
using AiHackApi.Repositories;
using AiHackApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

// Configurar a string de conex�o
string? connectionString = builder.Configuration.GetConnectionString("OracleConnection");

// Verificar se a string de conex�o n�o � nula ou vazia
if (string.IsNullOrEmpty(connectionString))
{
    // Lan�ar uma exce��o se a string de conex�o n�o for encontrada no appsettings.json
    throw new InvalidOperationException("A string de conex�o 'OracleConnection' n�o foi encontrada no appsettings.json.");
}

// Registrar o ApplicationDbContext
//configura o DbContext para usar Oracle com a string de conex�o configurada
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
//.EnableAnnotations() permite que anota��es sejam usadas para documentar rotas e m�todos
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AiHackApi",
        Version = "v1",
        Description = "API para gerenciamento de Consultas M�dicas da AiHack"
    });
    c.EnableAnnotations(); // Habilitar anota��es no Swagger
});

// Registrar reposit�rios e servi�os
// Aqui estao registrando todas as interfaces e implementa��es para reposit�rios e servi�os
// Utilizando o AddScoped, uma nova inst�ncia de cada servi�o/reposit�rio ser� criada por requisi��o HTTP
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

// Middleware global para tratamento de exce��es
// Middleware para capturar erros globais e retornar mensagens personalizadas
// Pode ser configurado para produzir uma resposta de erro gen�rica em caso de exce��es n�o tratadas
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError; // C�digo de erro 500
        context.Response.ContentType = "application/json"; // O retorno ser� em formato JSON

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            await context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                Details = contextFeature?.Error.Message ?? "Erro desconhecido" // Verifica��o de nulidade
            }.ToString() ?? ""); // Garante que o WriteAsync n�o receba null

        }
    });
});

//Configurar Swagger para UI e documenta��o
// Swagger gerar� a interface visual da API, acess�vel na raiz do site (/)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AiHackApi v1");
    c.RoutePrefix = string.Empty; // Faz com que o Swagger seja acess�vel na raiz
});

// Middleware de autoriza��o
// Autoriza��o garante que apenas usu�rios autenticados acessem endpoints protegidos
app.UseAuthorization();

// Mapear controladores
// Mapear todas as rotas definidas nos controladores
app.MapControllers();

// Executar a aplica��o
// Inicializa a aplica��o e come�a a escutar requisi��es HTTP
app.Run();
