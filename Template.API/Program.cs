using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Template.API.Filters;
using Template.API.Middlewares;
using Template.Application.Services.EmpresaServices.CreateEmpresa;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;
using Template.Infrastructure.Persistence.NHibernate;
using Template.Infrastructure.Persistence.NHibernate.Extensions;

var builder = WebApplication.CreateBuilder(args);

var sqliteFileName = "TemplateDB.sqlite";
var connString = $"Data Source={sqliteFileName};Version=3;New=True;";

SQLiteExtensions.ConfigureDB(sqliteFileName, connString);

builder.Services.AddNHibernate(connString);
builder.Services.AddScoped<IUnitOfWork, NHUnitOfWork>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Template.Application.SeedWork.IRequest<>).GetTypeInfo().Assembly));

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
