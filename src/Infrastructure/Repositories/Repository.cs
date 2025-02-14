using Domain.Entities;
using Domain.Repositories;
using NHibernate.Linq;

namespace Infrastructure.Repositories;

internal class Repository<T> : IRepository<T> where T : Entity
{
    public Repository(NHibernate.ISession session)
    {
        Session = session;
    }

    protected NHibernate.ISession Session { get; }

    public async Task Add(T entity)
    {
        _ = await Session.SaveAsync(entity);
    }

    public async Task Delete(int id)
    {
        T? entity = await Get(id);
        if (entity is not null)
        {
            await Session.DeleteAsync(entity);
        }
    }

    public async Task<T?> Get(int id)
    {
        return await Session.GetAsync<T>(id);
    }

    protected IQueryable<T> GetAllQueryable()
    {
        return Session.Query<T>();
    }

    public async Task<ICollection<T>> GetAll()
    {
        return await GetAllQueryable().ToListAsync();
    }

    public async Task Update(T entity)
    {
        await Session.UpdateAsync(entity);
    }
}
