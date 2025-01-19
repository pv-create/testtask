using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Todo.Core.Interfaces;
using Todo.Core.Services;

namespace Todo.Core
{
    public static class Dependencies
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITodoService, TodoService>();
            builder.Services.AddScoped<IFileService, FileService>();
        }
    }
}