using AppStore.Models.Domain;
using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation;

public class CategoryService : ICategoryService
{

    private readonly DatabaseContext _databasecontext;
    public CategoryService(DatabaseContext databasecontext)
    {
        _databasecontext = databasecontext;
    }

    public IQueryable<Category> List()
    {
        return _databasecontext.Categories.AsQueryable();
    }
}