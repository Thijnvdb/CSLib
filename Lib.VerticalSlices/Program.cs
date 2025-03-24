using Lib.VerticalSlices;
using Lib.VerticalSlices.CreatePerson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder
    .Services
    .AddOpenApi()
    .AddSliceDependenciesFromAssembly<CreatePerson>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app
    .AddSliceEndpointsFromAssembly<CreatePerson>()
    .Run();
