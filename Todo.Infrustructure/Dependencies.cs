using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Core.Contracts;
using Todo.Infrustructure.Repositories;

namespace Todo.Infrustructure
{
    public static class Dependencies
    {
        public static void AddTodoDb(this WebApplicationBuilder builder)
        {
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Строка подключения 'DefaultConnection' не найдена.");
            }

            builder.Services.AddDbContext<TodoDbContext>(options =>
                options.UseNpgsql(connectionString));

            builder.Services.AddScoped<ItodoRepository, TodoRepository>();
        }
    }
}