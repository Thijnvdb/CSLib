namespace Lib.Specifications;

public static class SpecificationBuilder
{
    public static Specification<T> Not<T>(ISpecification<T> specification) => new NotSpecification<T>(specification);
    public static Specification<T> And<T>(params ICollection<ISpecification<T>> specifications) => new AndSpecification<T>(specifications);
    public static Specification<T> Or<T>(params ICollection<ISpecification<T>> specifications) => new OrSpecification<T>(specifications);
}