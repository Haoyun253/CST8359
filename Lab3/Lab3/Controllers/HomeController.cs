using Lab3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SongForm() => View();

        [HttpPost]
        public IActionResult Sing(int monkeys)
        {
            ViewData["monkeys"] = monkeys;
            return View(monkeys);
        }

        public IActionResult CreateStudent() => View();

        [HttpPost]
        public IActionResult DisplayStudent(Student student)
        {
            return View(student);
        }
        public IActionResult Error()
        {
            return View();
        }

    }
}
