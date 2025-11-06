namespace AppStore.Repositories.Abstract
{
    public interface IFileService
    {
        public Tuple<int, string> SaveFile(IFormFile file);
        public bool DeleteFile(string filename);
    }
}