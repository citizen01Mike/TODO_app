using TODOList.TodoCore.Models.Entities;
using TODOList.TodoCore.Services.Interface;

namespace TODOList.TodoServices
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _dbContext;

        public TodoService(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Todo> Search(string searchTerm)
        {
            searchTerm = searchTerm.Trim().ToLower();

            return _dbContext.TodoItems
                .Where(t => 
                    t.Name.Contains(searchTerm) || 
                    t.Description.Contains(searchTerm) ||
                    t.Amount.ToString().Contains(searchTerm))
                .ToList();
        }
    }
}
