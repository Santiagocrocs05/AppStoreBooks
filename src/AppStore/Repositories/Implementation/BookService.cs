using AppStore.Models.Domain;
using AppStore.Models.DTOs;
using AppStore.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AppStore.Repositories.Implementation;

public class BookService : IBookService
{
    private readonly DatabaseContext _databasecontext;

    public BookService(DatabaseContext databasecontext)
    {
        _databasecontext = databasecontext;
    }
    public bool Add(Book book)
    {
        try
        {
            _databasecontext.Books.Add(book);
            _databasecontext.SaveChanges();
            foreach (var categoryId in book.Categories!)
            {
                var BookCategory = new BookCategory
                {
                    BookId = book.Id,
                    CategoryId = categoryId
                };
                _databasecontext.BookCategories!.Add(BookCategory);
            }
            _databasecontext.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            var book = _databasecontext.Books.FirstOrDefault(x => x.Id == id);
            if (book is null)
            {
                return false;
            }

            var BookCategories = _databasecontext.BookCategories.Where(a => a.BookId == book.Id);
            _databasecontext.BookCategories.RemoveRange(BookCategories);
            _databasecontext.Books.Remove(book);
            _databasecontext.SaveChanges();
            return true;
        }
        catch (System.Exception)
        {

            return false;
        }
    }

    public Book GetBookById(int id)
    {
        var book = _databasecontext.Books.FirstOrDefault(x => x.Id == id);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {id} was not found.");
        }
        return book;
    }

    public List<int> GetCategoryByBookId(int bookid)
    {
        return _databasecontext.BookCategories.Where(a => a.BookId == bookid).Select(a => a.CategoryId).ToList();
    }

    public BookListvm List(string term = "", bool paging = false, int currentpage = 0)
    {
        var data = new BookListvm();

        var list = _databasecontext.Books.ToList();

        if (string.IsNullOrEmpty(term))
        {
            term = term.ToLower();
            list = list.Where(a => a.Title.ToLower().StartsWith(term)).ToList();
        }

        if (paging)
        {
            int pagesize = 5;
            int count = list.Count;
            int totalPages = (int)Math.Ceiling(count / (double)pagesize);
            list = list.Skip((currentpage - 1) * pagesize).Take(pagesize).ToList();
            data.Pagesize = pagesize;
            data.currentpage = currentpage;
            data.TotalPages = totalPages;
        }

        foreach (var book in list)
        {
            var categories = (

                from category in _databasecontext.Categories
                join bc in _databasecontext.BookCategories
                on category.Id equals bc.CategoryId
                where bc.BookId == book.Id
                select category.Name
            ).ToList();

            var categoryNames = string.Join(",", categories);

            book.CategoriesName = categoryNames;
        }

        data.BookList = list.AsQueryable();

        return data;
    }

    public bool Update(Book book)
    {
        try
        {
            var categoriesdelete = _databasecontext.BookCategories.Where(p => p.BookId == book.Id);
            foreach (var categorie in categoriesdelete)
            {
                _databasecontext.BookCategories.Remove(categorie);
            }
            foreach (var categoryid in book.Categories!)
            {
                var librocategory = new BookCategory { CategoryId = categoryid, BookId = book.Id };
                _databasecontext.BookCategories.Add(librocategory);
                _databasecontext.SaveChanges();
            }

            _databasecontext.Books.Update(book);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}