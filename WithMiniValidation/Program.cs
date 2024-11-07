using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var group = app.MapGroup("/people")
    .WithOpenApi()
    .WithParameterValidation();

group.MapPost("/", (Person person) => TypedResults.Created(null as string, person));
    
app.Run();

record Person([Required]string Forename, [Required]string Surname);