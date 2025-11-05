using AppStore.Models.Domain;

namespace AppStore.Models.DTOs;


public class BookListvm
{
    public IQueryable<Book>? BookList { get; set; }
    public int Pagesize { get; set; }

    public int currentpage { get; set; }

    public int TotalPages { get; set; }

    public string? term { set; get; }
}
