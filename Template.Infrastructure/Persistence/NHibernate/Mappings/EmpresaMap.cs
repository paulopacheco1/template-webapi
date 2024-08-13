using FluentNHibernate.Mapping;
using Template.Domain.Aggregates.EmpresaAggregate;

namespace Template.Infrastructure.Persistence.NHibernate.Mappings;

public class EmpresaMap : ClassMap<Empresa>
{
    public EmpresaMap()
    {
        Table("COMPANIES");

        Id(obj => obj.Id).Column("COMP_ID");
        Map(obj => obj.CreatedAt).Column("COMP_CREATED_AT");
        Map(obj => obj.UpdatedAt).Column("COMP_UPDATED_AT");
        Map(obj => obj.DeletedAt).Column("COMP_DELETED_AT");
        Map(obj => obj.Nome).Column("COMP_NAME");
        Map(obj => obj.Email).Column("COMP_EMAIL");

        HasMany(obj => obj.Filiais).Cascade.All().Not.LazyLoad().Where("SUBS_DELETED_AT is null");
    }
}

public class EmpresaFilialMap : ClassMap<EmpresaFilial>
{
    public EmpresaFilialMap()
    {
        Table("SUBSIDIARIES");

        Id(obj => obj.Id).Column("SUBS_ID");
        Map(obj => obj.CreatedAt).Column("SUBS_CREATED_AT");
        Map(obj => obj.UpdatedAt).Column("SUBS_UPDATED_AT");
        Map(obj => obj.DeletedAt).Column("SUBS_DELETED_AT");
        Map(obj => obj.Nome).Column("SUBS_NAME");

        Component<EmpresaEndereco>(obj => obj.Endereco, cp =>
        {
            cp.Map(obj => obj.CEP, "SUBS_ZIPCODE");
            cp.Map(obj => obj.UF, "SUBS_STATE");
            cp.Map(obj => obj.Cidade, "SUBS_CITY");
            cp.Map(obj => obj.Bairro, "SUBS_NEIGHBORHOOD");
            cp.Map(obj => obj.Logradouro, "SUBS_STREET");
            cp.Map(obj => obj.Numero, "SUBS_NUMBER");
            cp.Map(obj => obj.Complemento, "SUBS_COMPLEMENT");
        });

        References(x => x.Empresa).Column("SUBS_COMP_ID").ForeignKey("COMP_ID");
    }
}
