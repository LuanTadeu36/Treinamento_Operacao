using AutoMapper;
using TreinamentoAPI.Interfaces.Bancos;
using TreinamentoAPI.Interfaces.Servicos;
using TreinamentoAPI.Mappings;
using TreinamentoAPI.Repositorios;
using TreinamentoAPI.Servicos;

var builder = WebApplication.CreateBuilder(args);

// Configurar pol�tica CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Se precisar de suporte a cookies
    });
});

// Adicionar servi�os ao cont�iner.
builder.Services.AddControllers();
// Saiba mais sobre como configurar o Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inje��o de depend�ncia dos reposit�rios
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<IProdutoServico, ProdutoServico>();

builder.Services.AddScoped<IFuncionarioRepositorio, FuncionarioRepositorio>();
builder.Services.AddScoped<IFuncionarioServico, FuncionarioServico>();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configurar o pipeline de requisi��es HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyAllowSpecificOrigins"); 

app.UseAuthorization();

app.MapControllers();

app.Run();
