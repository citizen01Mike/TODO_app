using Microsoft.EntityFrameworkCore;

namespace TODOList.TodoServices
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        public DbSet<TodoCore.Models.Entities.Todo> TodoItems { get; set; }
    }
}
