using AppStore.Models.Domain;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppStore.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookservice;
    private readonly IFileService _fileservice;
    private readonly ICategoryService _categoryService;

    public BookController(IBookService bookservice,
    IFileService fileservice,
    ICategoryService categoryService)
    {
        _bookservice = bookservice;
        _fileservice = fileservice;
        _categoryService = categoryService;
    }

    [HttpPost]
    public IActionResult Add(Book book)
    {
        book.CategoriesList = _categoryService.List().Select(a => new SelectListItem
        { Text = a.Name, Value = a.Id.ToString() });

        if (!ModelState.IsValid)
        {
            return View(book);
        }

        if (book.File != null)
        {
            var result = _fileservice.SaveFile(book.File);
            if (result.Item1 == 0)
            {
                TempData["msg"] = "imagen no guardad";
                return View(book);
            }
            var filename = result.Item2;
            book.Imagen = filename;
        }

        var resultbook = _bookservice.Add(book);

        if (resultbook)
        {
            TempData["msg"] = "se agrego el libro";

            return RedirectToAction(nameof(Add));
        }
        TempData["msg"] = "Error al guardar el libro";
        return View(book);
    }

    public IActionResult Add()
    {
        var book = new Book();
        book.CategoriesList = _categoryService.List().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
        return View(book);
    }

    public IActionResult Edit(int id)
    {
        return View();
    }

    public IActionResult BookList()
    {
        return View();
    }

    public IActionResult Delete(int id)
    {
        return RedirectToAction(nameof(BookList));
    }
}