using Todo.Infrustructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSwaggerConfiguration(builder.Configuration);

builder.AddTodoDb();

var app = builder.Build();


app.MapOpenApi();

app.UseSwaggerConfiguration();

app.UseHttpsRedirection();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}