using _123Vendas.Vendas.Infrastructure.Extensions;
using _123Vendas.Vendas.Infrastructure.RabbitMQ.Settings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// RabbitMQ Configuration
builder.Services.Configure<RabbitMQConfiguracao>(builder.Configuration.GetSection("RabbitMQ"));

// Add services to the container.
builder.Services.AddAutoMapper();
builder.Services.AddServicesAndRepositories();
builder.Services.AddControllers();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
