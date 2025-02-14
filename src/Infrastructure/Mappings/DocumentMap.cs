using Domain.Entities;
using FluentNHibernate.Mapping;

namespace Infrastructure.Mappings;

public class DocumentMap : ClassMap<Document>
{
    public DocumentMap()
    {
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.Name).Length(255).Not.Nullable();
        Map(x => x.CreateDate).Not.Nullable();
    }
}
