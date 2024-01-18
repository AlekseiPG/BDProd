using BDProd.Data;
using BDProd.Models;

namespace BDProd.Services;

public interface IImageService
{
    IEnumerable<Image> GetImages();
    void ClassifyImage(int imageId, string classification);
    void MoveImage(int imageId, string destinationFolder);
    void UploadImage(IFormFile file, string destinationFolder);
}

public class ImageService : IImageService
{
    private readonly BDProdContext _dbContext;

    public ImageService(BDProdContext dbContext)
    {
        _dbContext = dbContext;
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
        // Например, использование System.IO для сохранения файла
        // var filePath = Path.Combine(destinationFolder, file.FileName);
        // using (var stream = new FileStream(filePath, FileMode.Create))
        // {
        //     file.CopyTo(stream);
        // }

        // Создаем новую запись в базе данных для изображения
        var newImage = new Image
        {
            ImageName = file.FileName,
            //ImagePath = filePath // Здесь присваиваем путь к файлу в базе данных
        };
        _dbContext.Images.Add(newImage);
        _dbContext.SaveChanges();
    }
}