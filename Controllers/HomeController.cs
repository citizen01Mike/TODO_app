using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TODOList.TodoCore.Models.ViewModels;
using TODOList.TodoServices;

namespace TODOList.Controllers
{
    public class HomeController : Controller
    {
        private readonly TodoDbContext _dbContext;

        public HomeController(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
