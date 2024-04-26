using Microsoft.EntityFrameworkCore;

namespace TODOList.Todo.Services.DbContext
{
    public class TodoDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        public DbSet<Core.Models.Entities.Todo> TodoItems { get; set; }
    }
}
