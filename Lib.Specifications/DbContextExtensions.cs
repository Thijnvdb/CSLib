using Microsoft.EntityFrameworkCore;

namespace Lib.Specifications;

public static class DbContextExtensions
{
    public static IQueryable<T> FindBySpecification<T>(this DbContext context, ISpecification<T> specification) where T : class
    {
        return context.Set<T>().Where(e => specification.IsSatisfiedBy(e));
    }
}