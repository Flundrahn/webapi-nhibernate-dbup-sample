using Domain.Entities;

namespace Domain.Repositories;

public interface IRepository<T> where T : Entity
{
    public Task<T?> Get(int id);
    public Task<ICollection<T>> GetAll();
    public Task Add(T entity);
    public Task Update(T entity);
    public Task Delete(int id);
}
