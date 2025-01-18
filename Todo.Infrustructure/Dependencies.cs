using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.Infrustructure
{
    public static class Dependencies
    {
        public static void AddTodoDb(this WebApplicationBuilder builder)
        {
            // Получаем строку подключения из конфигурации
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Строка подключения 'DefaultConnection' не найдена.");
            }

            Console.WriteLine($"Connection String: {connectionString}");

            // Настраиваем DbContext
            builder.Services.AddDbContext<TodoDbContext>(options =>
                options.UseNpgsql(connectionString));
        }

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using TodoDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<TodoDbContext>();

            dbContext.Database.Migrate();
        }
    }
}