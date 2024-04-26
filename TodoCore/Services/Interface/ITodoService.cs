using TODOList.TodoCore.Models.Entities;

namespace TODOList.TodoCore.Services.Interface
{
    public interface ITodoService
    {
        public List<Todo> Search(string searchTerm);
    }
}
