using Domain.Entities;
using FluentNHibernate.Mapping;

namespace Infrastructure.Mappings;

public class DocumentMap : ClassMap<Document>
{
    public DocumentMap()
    {
        Id(x => x.Id);
        Map(x => x.Name);
        Map(x => x.CreateDate);
    }
}
