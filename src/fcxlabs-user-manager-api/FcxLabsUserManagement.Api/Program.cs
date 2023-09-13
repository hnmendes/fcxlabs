using System.Reflection;
using FcxLabsUserManagement.Application;
using FcxLabsUserManagement.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Secrets
builder.Configuration.AddEnvironmentVariables().AddUserSecrets(Assembly.GetExecutingAssembly(), true);

//Adding App Dependencies
builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
