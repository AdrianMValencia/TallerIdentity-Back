using TallerIdentity.Application.Commons.Bases;

namespace TallerIdentity.Application.Interfaces.Services;

public interface IOrderingQuery
{
    IQueryable<T> Ordering<T>(BasePagination request, IQueryable<T> queryable) where T : class;
}
