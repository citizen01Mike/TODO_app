using Microsoft.EntityFrameworkCore;
using TODOList.Models.Entities;

namespace TODOList.Migrations.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }

        public DbSet<Todo> TodoItems { get; set; }
    }
}
