namespace Lib.VerticalSlices.CreatePerson;

public class CreatePerson : ISlice
{
    public record CreatePersonRequest(string Name);
    public record Person(string Name);

    public class Endpoint() : IEndpoint<CreatePersonRequest, Person>
    {
        ICollection<Person> People = [];
        public async Task<Person> HandleAsync(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            var person = new Person(request.Name);
            People.Add(person);
            return person;
        }
    }

    public void RegisterDependencies(IServiceCollection services)
    {
        services.AddScoped<IEndpoint<CreatePersonRequest, Person>, Endpoint>();
    }

    public void RegisterEndpoints(WebApplication app)
    {
        app.MapPost("CreatePerson", async (CreatePersonRequest request, IEndpoint<CreatePersonRequest, Person> endpoint) =>
        {
            return await endpoint.HandleAsync(request, CancellationToken.None);
        });
    }
}
