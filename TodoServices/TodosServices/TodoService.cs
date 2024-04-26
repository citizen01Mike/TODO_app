﻿using TODOList.TodoCore.Models.Entities;
using TODOList.TodoCore.Services.Interface;
using TODOList.TodoServices;

namespace TODOList.Services.Todos
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
            if (!string.IsNullOrEmpty(searchTerm))
            {
                return _dbContext.TodoItems
                .Where(t => t.Name.Contains(searchTerm) || t.Description.Contains(searchTerm))
                .ToList();
            }

            return _dbContext.TodoItems.ToList();
        }
    }
}