using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODOList.TodoCore.Models.Entities;
using TODOList.TodoCore.Models.ViewModels;
using TODOList.TodoCore.Services.Interface;
using TODOList.TodoServices;

namespace TODOList.Controllers
{
    public class TodosController : Controller
    {
        private readonly TodoDbContext _dbContext;
        private readonly ITodoService _todoService;

        public TodosController(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add(TodoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var todo = new Todo
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    IsCompleted = false
                };

                await _dbContext.TodoItems.AddAsync(todo);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("List");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var todos = await _dbContext.TodoItems.ToListAsync();

            return View(todos);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var todo = await _dbContext.TodoItems.FindAsync(id);

            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Todo todos)
        {
            var todo = await _dbContext.TodoItems.FindAsync(todos.Id);

            if (todo is not null)
            {
                todo.Name = todos.Name;
                todo.Description = todos.Description;
                todo.IsCompleted = todos.IsCompleted;

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("List", "Todos");
            }

            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Todo todos)
        {
            var todo = await _dbContext.TodoItems
                .AsNoTracking()
                .SingleOrDefaultAsync(v => v.Id == todos.Id);

            if (todo is not null)
            {
                _dbContext.TodoItems.Remove(todos);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Todos");
        }

        public async Task<IActionResult> MarkAsDone(Todo todos)
        {
            var todo = await _dbContext.TodoItems.SingleOrDefaultAsync(t => t.Id == todos.Id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsCompleted = true;
            _dbContext.Update(todo);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Todos");
        }

        public IActionResult SearchTodo(string searchTerm)
        {
            var results = _todoService.Search(searchTerm);
            return View("Search");  // Assuming you want to reuse the Index view to display results
        }
    }
}
