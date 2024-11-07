using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<Person>, PersonValidator>();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var group = app.MapGroup("/people")
    .WithOpenApi()
    .AddFluentValidationAutoValidation();
group.MapPost("/", (Person person) => TypedResults.Created(null as string, person));
    
app.Run();

public record Person(string Forename, string Surname);

public class PersonValidator : AbstractValidator<Person> 
{
    public PersonValidator()
    {
        RuleFor(x => x.Forename).NotNull();
        RuleFor(x => x.Surname).NotNull();
    }
}