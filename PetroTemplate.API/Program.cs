using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PetroTemplate.API.Filters;
using PetroTemplate.API.Middlewares;
using PetroTemplate.Application.Services.EmpresaServices.CreateEmpresa;
using PetroTemplate.Domain.Repositories;
using PetroTemplate.Infrastructure.Persistence.EFCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EFDataContext>(
    dbContextOptions => dbContextOptions
        .UseInMemoryDatabase(databaseName: "PetroTemplateDb")
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableDetailedErrors()
);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(PetroTemplate.Application.SeedWork.IRequest<>).GetTypeInfo().Assembly));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());

builder.Services.AddValidatorsFromAssemblyContaining<CreateEmpresaRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
