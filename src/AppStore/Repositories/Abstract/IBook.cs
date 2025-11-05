using AppStore.Models.Domain;
using AppStore.Models.DTOs;

namespace AppStore.Repositories.Abstract;


public interface IBookService
{
    bool Add(Book book);

    bool Update(Book book);

    Book GetBookById(int id);

    bool Delete(int id);

    BookListvm Listvm(string term = "", bool paging = false, int currentpage = 0);

    List<int> GetCategoryByBookId(int bookid);
}

