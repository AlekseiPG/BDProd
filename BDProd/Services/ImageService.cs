using BDProd.Data;
using BDProd.Models;
using Microsoft.EntityFrameworkCore;

namespace BDProd.Services;

public interface IImageService
{
    IEnumerable<Image> GetImages();
    void ClassifyImage(int imageId, string classification);
    void MoveImage(int imageId, string destinationFolder);
    void UploadImage(IFormFile file, string destinationFolder);

    IEnumerable<string> GetFolders(string rootFolder);
    IEnumerable<Image> GetImagesInFolder(string folderPath);
    IEnumerable<Image> GetSelectedImages(List<int> imageIds);
}

public class ImageService : IImageService
{
    private readonly BDProdContext _dbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageService(BDProdContext dbContext, IWebHostEnvironment webHostEnvironment)
    {
        _dbContext = dbContext;
        _webHostEnvironment = webHostEnvironment;
    }

    public IEnumerable<Image> GetImages()
    {
        return _dbContext.Images.ToList();
    }

    public void ClassifyImage(int imageId, string classification)
    {
        var image = _dbContext.Images.Find(imageId);
        //if (image != null)
        //{
        //    image.Classification = classification;
        //    _dbContext.SaveChanges();
        //}
    }

    public void MoveImage(int imageId, string destinationFolder)
    {
        var image = _dbContext.Images.Find(imageId);
        if (image != null)
        {
            // Логика перемещения изображения в другую папку
            // Например, использование System.IO для перемещения файлов
            // string sourcePath = image.FilePath;
            // string destinationPath = Path.Combine(destinationFolder, image.FileName);
            // System.IO.File.Move(sourcePath, destinationPath);

            // После перемещения обновляем базу данных
            // image.FilePath = destinationPath;
            _dbContext.SaveChanges();
        }
    }

    public void UploadImage(IFormFile file, string destinationFolder)
    {
        // Логика загрузки изображения
        var uploadsFolder = Path.Combine("/TestingImages", destinationFolder);

        // Генерация уникального имени файла, чтобы избежать конфликтов
        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        // Создаем новую запись в базе данных для изображения
        var newImage = new Image
        {
            ImageName = uniqueFileName,
            ImagePath = filePath // Здесь присваиваем путь к файлу в базе данных
        };

        _dbContext.Images.Add(newImage);
        _dbContext.SaveChanges();
    }


    public IEnumerable<string> GetFolders(string rootFolder)
    {
        try
        {
            var folders = Directory.GetDirectories(rootFolder);
            return folders.Select(folder => Path.GetFileName(folder));
        }
        catch (Exception ex)
        {
            return Enumerable.Empty<string>();
        }
    }


    public IEnumerable<Image> GetImagesInFolder(string folderPath)
    {
        try
        {
            var images = Directory.GetFiles(folderPath)
                              .Where(filePath => filePath.EndsWith(".jpg") || filePath.EndsWith(".png"))
                              .Select(filePath => new Image
                              {
                                  ImageName = Path.GetFileName(filePath),
                                  ImagePath = filePath
                              });
            return images;
        }
        catch (Exception ex)
        {
            return Enumerable.Empty<Image>();
        }
    }

    public IEnumerable<Image> GetSelectedImages(List<int> imageIds)
    {
        try
        {
            var selectedImages = _dbContext.Images.Where(image => imageIds.Contains(image.Id));
            return selectedImages.ToList();
        }
        catch (Exception ex)
        {
            return Enumerable.Empty<Image>();
        }
    }

}

