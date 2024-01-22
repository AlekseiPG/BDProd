using BDProd.Services;
using Microsoft.AspNetCore.Mvc;

namespace BDProd.Controllers
{
    public class ImageManagementController : Controller
    {
        private readonly IImageService _imageService;

        public ImageManagementController(IImageService imageService)
        {
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            var folders = _imageService.GetFolders("C:\\Changes_C\\Projects\\TESTING");
            return View(folders);
        }

        [HttpPost]
        public IActionResult DisplayImages(string folderPath)
        {
            var images = _imageService.GetImagesInFolder(folderPath);
            return View(images);
        }

        [HttpPost]
        public IActionResult SelectImages(List<int> selectedImageIds)
        {
            var selectedImages = _imageService.GetSelectedImages(selectedImageIds);
            return View(selectedImages);
        }

    }
}
