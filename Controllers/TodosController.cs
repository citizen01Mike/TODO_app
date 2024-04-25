using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODOList.Data;
using TODOList.Models.Entities;
using TODOList.Models.ViewModels;

namespace TODOList.Controllers
{
    public class TodosController : Controller
    {
        private readonly TodoDbContext _dbContext;

        public TodosController(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel viewModel)
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
        public async Task<IActionResult> Edit(Todo viewModel)
        {
            var todo = await _dbContext.TodoItems.FindAsync(viewModel.Id);

            if (todo is not null)
            {
                todo.Name = viewModel.Name;
                todo.Description = viewModel.Description;
                todo.IsCompleted = viewModel.IsCompleted;

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("List", "Todos");
            }

            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Todo viewModel)
        {
            var todo = await _dbContext.TodoItems
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == viewModel.Id);

            if (todo is not null)
            {
                _dbContext.TodoItems.Remove(viewModel);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Todos");
        }

        public async Task<IActionResult> MarkAsDone(Todo viewModel)
        {
            var todo = await _dbContext.TodoItems.FirstOrDefaultAsync(t => t.Id == viewModel.Id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsCompleted = true;
            _dbContext.Update(todo);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Todos");
        }
    }
}
