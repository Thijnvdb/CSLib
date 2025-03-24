using Lib.Specifications;
using Microsoft.EntityFrameworkCore;

public interface IRepository
{
    public IQueryable<T> Find<T>(ISpecification<T> specification) where T : class;
}

internal class Repository : IRepository
{
    private readonly DbContext _dbContext;
    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> Find<T>(ISpecification<T> specification) where T : class
    {
        return _dbContext.FindBySpecification(specification);
    }
}