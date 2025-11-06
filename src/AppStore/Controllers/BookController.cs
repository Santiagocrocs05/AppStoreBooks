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
        var book = _bookservice.GetBookById(id);
        var categoriesbook = _bookservice.GetCategoryByBookId(id);
        var MultiselectList = new MultiSelectList(_categoryService.List(), "Id", "Name", categoriesbook);
        book.MulticategoriesList = MultiselectList;
        return View(book);
    }

    [HttpPost]
    public IActionResult Edit(Book book)
    {
        var categoriesbook = _bookservice.GetCategoryByBookId(book.Id);
        var MultiselectList = new MultiSelectList(_categoryService.List(), "Id", "Name", categoriesbook);
        book.MulticategoriesList = MultiselectList;

        if (!ModelState.IsValid)
        {
            return View(book);
        }
        if (book.File != null)
        {
            var fileresult = _fileservice.SaveFile(book.File);
            if (fileresult.Item1 == 0)
            {
                TempData["msg"] = "la imagen no fue guardada";
                return View(book);
            }

            var filename = fileresult.Item2;
            book.Imagen = filename;
        }

        var resultadobook = _bookservice.Update(book);
        if (!resultadobook)
        {
            TempData["msg"] = "no se puedo actualizar el libro";
            return View(book);
        }

        TempData["msf"] = "se actualizo el libro";
        return View(book);
    }
    public IActionResult BookList()
    {
        var books = _bookservice.List();
        return View(books);
    }

    public IActionResult Delete(int id)
    {
        var delete = _bookservice.Delete(id);
        return RedirectToAction(nameof(BookList));
    }
}