using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Todo.Infrustructure; // Замените на ваш namespace

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Получаем строку подключения из переменных окружения
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Строка подключения не найдена в переменных окружения.");
        }

        Console.WriteLine($"Using connection string: {connectionString}");

        // Регистрируем DbContext
        services.AddDbContext<TodoDbContext>(options =>
            options.UseNpgsql(connectionString));
    })
    .Build();

using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    Console.WriteLine("Applying migrations...");
    var dbContext = services.GetRequiredService<TodoDbContext>();
    dbContext.Database.Migrate();
    Console.WriteLine("Migrations applied successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
    throw;
}