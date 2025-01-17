using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Core.Models;

namespace Todo.Infrustructure
{
    public class TodoDbContext: DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoFile> TodoFiles { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}