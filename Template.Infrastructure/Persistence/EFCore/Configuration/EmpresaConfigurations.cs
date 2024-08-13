using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Aggregates.EmpresaAggregate;

namespace Template.Infrastructure.Persistence.EFCore.Configuration;

public class EmpresaConfigurations : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.HasKey(obj => obj.Id);
        
        builder
            .HasMany(obj => obj.Filiais)
            .WithOne(obj => obj.Empresa)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class EmpresaFilialConfigurations : IEntityTypeConfiguration<EmpresaFilial>
{
    public void Configure(EntityTypeBuilder<EmpresaFilial> builder)
    {
        builder.HasKey(obj => obj.Id);
        
        builder.OwnsOne(obj => obj.Endereco);
    }
}
