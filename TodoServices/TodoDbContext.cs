using Microsoft.EntityFrameworkCore;
using TODOList.TodoCore.Models.Entities;

namespace TODOList.TodoServices
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> TodoItems { get; set; }
    }
}
