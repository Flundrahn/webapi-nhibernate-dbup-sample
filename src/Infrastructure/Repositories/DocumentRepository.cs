using Domain.Entities;
using Domain.Repositories;
using NHibernate;

namespace Infrastructure.Repositories
{
    internal class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(ISession session) : base(session)
        {
        }
    }
}
