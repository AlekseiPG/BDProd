using BDProd.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace BDProd.Controllers
{
    public class ImagesAltController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImagesAltController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            //var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "TestingImages");
            var folderPath = "C:\\Changes_C\\Projects\\TESTING";
            var imageFolders = LoadImageFoldersFromServer(folderPath);
            return View(imageFolders);
        }

        private List<FancyTreeNode> LoadImageFoldersFromServer(string folderPath)
        {
            var fancyTreeNodes = new List<FancyTreeNode>();

            if (Directory.Exists(folderPath))
            {
                var subdirectories = Directory.GetDirectories(folderPath);

                foreach (var subdirectory in subdirectories)
                {
                    var folderNode = new FancyTreeNode
                    {
                        title = Path.GetFileName(subdirectory),
                        folder = true,
                        children = new List<FancyTreeNode>()
                    };

                    //var imageFiles = Directory.GetFiles(subdirectory, "*.png"); // Расширение может быть изменено
                    var imageFiles = Directory.GetFiles(subdirectory)
                        .Where(filePath => filePath.EndsWith(".jpg") || filePath.EndsWith(".png"));

                    foreach (var imageFile in imageFiles)
                    {
                        folderNode.children.Add(new FancyTreeNode
                        {
                            title = Path.GetFileNameWithoutExtension(imageFile),
                            //path = "/TestingImages" + imageFile.Replace(folderPath, "").Replace("\\", "/")
                            //path = Path.GetFullPath(imageFile)
                            //path = imageFile.Replace(folderPath, "").Replace("\\", "/")
                            path = imageFile
                        });
                    }

                    fancyTreeNodes.Add(folderNode);
                }
            }

            return fancyTreeNodes;
        }


        // Добавим дополнительный метод для обработки выбранных изображений
        [HttpPost]
        public IActionResult Continue(List<string> selectedImages)
        {
            // Обработка выбранных изображений, например, передача их на следующую страницу
            // для сохранения в базе данных.
            // ...

            return RedirectToAction("NextPage");
        }
    }
}
