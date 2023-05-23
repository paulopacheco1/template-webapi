﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PetroTemplate.Infrastructure.Persistence.EFCore;

public class EFDataContext : DbContext
{
    public EFDataContext(DbContextOptions<EFDataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

