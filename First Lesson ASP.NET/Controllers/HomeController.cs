using First_Lesson_ASP.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace First_Lesson_ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly List<Todo> _todos = new();
        private static int _id;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var orderedTodos = _todos.OrderBy(t => t.IsDone).ToList();
            _todos.Clear();
            orderedTodos.ForEach(todo => _todos.Add(todo));

            return View(_todos);
        }

        [HttpPost]
        public IActionResult AddTodo(Todo task)
        {
            task.Id = ++_id;
            _todos.Add(task);            
            return RedirectToAction(nameof(HomeController.Index));
        }

        
        [HttpGet]
        public IActionResult DeleteTodo(int id)
        {
            _todos.Remove(_todos.Find(t => t.Id == id));
            return RedirectToAction(nameof(HomeController.Index));
        }

        [HttpGet]
        public IActionResult CheckTodo(int id)
        {
            Todo todo = _todos.Find(t => t.Id == id);
            todo.IsDone = !todo.IsDone;
            return RedirectToAction(nameof(HomeController.Index));
        }
        public IActionResult Privacy()
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