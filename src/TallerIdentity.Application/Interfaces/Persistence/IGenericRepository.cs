using TallerIdentity.Domain.Entities;

namespace TallerIdentity.Application.Interfaces.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAllQueryable();
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    void Update(T entity);
    Task DeleteAsync(int id);
}
