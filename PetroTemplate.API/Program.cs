using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using PetroTemplate.API.Filters;
using PetroTemplate.API.Middlewares;
using PetroTemplate.Application.Services.EmpresaServices.CreateEmpresa;
using PetroTemplate.Domain.Repositories;
using PetroTemplate.Infrastructure.Persistence;
using PetroTemplate.Infrastructure.Persistence.NHibernate;
using PetroTemplate.Infrastructure.Persistence.NHibernate.Extensions;

var builder = WebApplication.CreateBuilder(args);

var sqliteFileName = "TemplateDB.sqlite";
var connString = $"Data Source={sqliteFileName};Version=3;New=True;";

SQLiteExtensions.ConfigureDB(sqliteFileName, connString);

builder.Services.AddNHibernate(connString);
builder.Services.AddScoped<IUnitOfWork, NHUnitOfWork>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(PetroTemplate.Application.SeedWork.IRequest<>).GetTypeInfo().Assembly));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());
builder.Services.AddValidatorsFromAssemblyContaining<CreateEmpresaRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandler();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
