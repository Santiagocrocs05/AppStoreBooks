using AppStore.Models.Domain;

namespace AppStore.Repositories.Abstract;

public interface ICategoryService
{
    IQueryable<Category> List();
}