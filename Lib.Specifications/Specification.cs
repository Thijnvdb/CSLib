using System.Linq.Expressions;

namespace Lib.Specifications;

public interface ISpecification<TEntity>
{
    bool IsSatisfiedBy(TEntity input);
}

public abstract class Specification<TEntity> : ISpecification<TEntity>
{
    protected abstract Expression<Func<TEntity, bool>> Expression { get; } // for logging and such
    private Func<TEntity, bool> _compiled => Expression.Compile();

    public Specification()
    {
    }

    public bool IsSatisfiedBy(TEntity input) => _compiled(input);

    public Specification<TEntity> And(params ICollection<ISpecification<TEntity>> others)
    {
        return new AndSpecification<TEntity>([this, .. others]);
    }

    public Specification<TEntity> Or(params ICollection<ISpecification<TEntity>> others)
    {
        return new OrSpecification<TEntity>([this, .. others]);
    }
}

public class NotSpecification<TEntity> : Specification<TEntity>
{
    protected override Expression<Func<TEntity, bool>> Expression { get; }
    public NotSpecification(ISpecification<TEntity> inner)
    {
        Expression = (input) => !inner.IsSatisfiedBy(input);
    }
}

public class AndSpecification<TEntity> : Specification<TEntity>
{
    protected override Expression<Func<TEntity, bool>> Expression { get; }

    public AndSpecification(params ICollection<ISpecification<TEntity>> specifications) : base()
    {
        Expression = input => specifications.All(spec => spec.IsSatisfiedBy(input));
    }
}

public class OrSpecification<TEntity> : Specification<TEntity>
{
    protected override Expression<Func<TEntity, bool>> Expression { get; }

    public OrSpecification(params ICollection<ISpecification<TEntity>> specifications) : base()
    {
        Expression = input => specifications.All(spec => spec.IsSatisfiedBy(input));
    }
}