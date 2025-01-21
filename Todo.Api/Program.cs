using Todo.Core;
using Todo.Infrustructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddSwaggerConfiguration(builder.Configuration);

builder.AddTodoDb();

builder.AddServices();

var app = builder.Build();


app.MapOpenApi();

app.UseSwaggerConfiguration();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();