using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AppStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        public HomeController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public IActionResult Index(string term = "", int currentPage = 1)
        {
            var books = _bookService.List(term, true, currentPage);
            return View(books);
        }
    }
}