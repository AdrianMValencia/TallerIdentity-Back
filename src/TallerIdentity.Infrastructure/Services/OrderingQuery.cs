using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using TallerIdentity.Application.Commons.Bases;
using TallerIdentity.Application.Interfaces.Services;

namespace TallerIdentity.Infrastructure.Services;

public class OrderingQuery : IOrderingQuery
{
    // Cache para evitar regenerar expresiones Lambda repetidamente
    private static readonly ConcurrentDictionary<string, LambdaExpression> ExpressionCache = new();

    // Aplica ordenamiento y paginación a un IQueryable
    public IQueryable<T> Ordering<T>(BasePagination request, IQueryable<T> queryable) where T : class
    {
        queryable = ApplyOrdering(queryable, request.Sort!, request.Order == "desc");
        return Paginate(queryable, request);
    }

    // Aplica el ordenamiento dinámicamente usando reflexión y expresiones
    private static IQueryable<T> ApplyOrdering<T>(IQueryable<T> queryable, string sortColumn, bool descending)
    {
        if (string.IsNullOrWhiteSpace(sortColumn))
            return queryable;

        var type = typeof(T);
        // Busca la propiedad a ordenar (case-insensitive)
        var property = type.GetProperty(sortColumn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property == null)
            return queryable;

        // Usa cache para evitar recrear la expresión si ya existe
        string cacheKey = $"{type.FullName}.{sortColumn}.{descending}";
        if (!ExpressionCache.TryGetValue(cacheKey, out var lambda))
        {
            var parameter = Expression.Parameter(type, "x");
            var propertyAccess = Expression.Property(parameter, property);
            lambda = Expression.Lambda(propertyAccess, parameter);
            ExpressionCache[cacheKey] = lambda;
        }

        // Llama dinámicamente a OrderBy u OrderByDescending
        string methodName = descending ? "OrderByDescending" : "OrderBy";
        var result = typeof(Queryable).GetMethods()
            .First(method => method.Name == methodName && method.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), property.PropertyType)
            .Invoke(null, [queryable, lambda]);

        return (IQueryable<T>)result!;
    }

    // Aplica paginación con Skip y Take
    private static IQueryable<T> Paginate<T>(IQueryable<T> queryable, BasePagination request)
    {
        return queryable.Skip((request.NumPage - 1) * request.Records).Take(request.Records);
    }
}
