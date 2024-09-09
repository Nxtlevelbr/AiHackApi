using AiHackApi.Data;
using AiHackApi.Repositories;
using AiHackApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar string de conexão
string? connectionString = builder.Configuration.GetConnectionString("OracleConnection");

// Verificar se a string de conexão não é nula ou vazia
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'OracleConnection' não foi encontrada no appsettings.json.");
}

// 2. Registrar o ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseOracle(connectionString);
});

// 3. Registrar Controladores
builder.Services.AddControllers();

// 4. Configurar Swagger (ativo em todas as fases)
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

// 5. Registrar repositórios e serviços
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

// 6. Middleware global para tratamento de exceções
app.UseExceptionHandler("/error");

// 7. Configurar Swagger para UI e documentação
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AiHackApi v1");
    c.RoutePrefix = string.Empty; // Swagger diretamente na raiz
});

// 8. Middleware de autorização
app.UseAuthorization();

// 9. Mapear controladores
app.MapControllers();

// 10. Executar a aplicação
app.Run();
