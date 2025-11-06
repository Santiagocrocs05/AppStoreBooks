using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _env;

    public FileService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public Tuple<int, string> SaveFile(IFormFile file)
    {
        try
        {
            var wwwpath = _env.WebRootPath;
            var path = Path.Combine(wwwpath, "uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var ext = Path.GetExtension(file.FileName);
            var allowedExts = new String[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExts.Contains(ext.ToLower()))
            {
                var messsage = $"solo estan permitidas las extensiones {allowedExts}";
                return new Tuple<int, string>(0, "Invalid file type");
            }

            var uniquestring = Guid.NewGuid().ToString();
            var newFilename = uniquestring + ext;

            var filepath = Path.Combine(path, newFilename);

            var stream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(stream);
            stream.Close();

            return new Tuple<int, string>(1, newFilename);


        }
        catch (Exception)
        {
            return new Tuple<int, string>(0, "Error saving file");
        }
    }

    public bool DeleteFile(string filename)
    {
       try
       {
            var wwwpath = _env.WebRootPath;
            var path = Path.Combine(wwwpath, "uploas/", filename);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
       }
       catch (Exception)
       {

            return false;
       }
    }
}