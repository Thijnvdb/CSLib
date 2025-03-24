using System.Linq.Expressions;
using static Lib.Specifications.SpecificationBuilder;

namespace Lib.Specifications;

public record Book(
    string Title,
    int ReleaseYear,
    string Author
);

public class AuthorSpecification(params ICollection<string> Names) : Specification<Book>
{
    protected override Expression<Func<Book, bool>> Expression => (book) => Names.Contains(book.Author);
}

public class ReleaseYearSpecification(int? minYear = null, int? maxYear = null) : Specification<Book>
{
    protected override Expression<Func<Book, bool>> Expression => (book) => book.ReleaseYear >= (minYear ?? 0) && book.ReleaseYear <= (maxYear ?? DateTime.Now.Year);
}

public class Test
{
    private IRepository _repository;
    public Test(IRepository repository)
    {
        _repository = repository;
    }

    public void TestMethod()
    {
        var spec = new AuthorSpecification();
        var speccedUp = _repository.Find(
            And(
                Not(new AuthorSpecification("Frank")),
                new ReleaseYearSpecification(maxYear: 2000)
            )
        );
    }
}