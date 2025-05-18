using Microsoft.EntityFrameworkCore;
using TallerIdentity.Application.Interfaces.Persistence;
using TallerIdentity.Domain.Entities;
using TallerIdentity.Infrastructure.Persistence.Context;

namespace TallerIdentity.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entity;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _entity = _context.Set<T>();
    }

    public IQueryable<T> GetAllQueryable()
    {
        var query = _entity.AsQueryable();
        return query;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var response = await _entity.ToListAsync();
        return response;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var response = await _entity.SingleOrDefaultAsync(x => x.Id == id);
        return response!;
    }

    public async Task CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public async Task DeleteAsync(int id)
    {
        T entity = await GetByIdAsync(id);
        _context.Remove(entity);
    }
}
