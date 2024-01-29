using System.IO;
using System.Linq;
using BDProd.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BDProd.Controllers
{
    public class TestController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TestController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var imageFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "TESTING");
            var imageFolders = LoadImageFoldersFromServer(imageFolderPath);
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
                    var relativePath = subdirectory.Substring(_webHostEnvironment.WebRootPath.Length).Replace('\\', '/');
                    var folderNode = new FancyTreeNode
                    {
                        title = Path.GetFileName(subdirectory),
                        folder = true,
                        children = new List<FancyTreeNode>(),
                        path = relativePath, 
                        key = relativePath 
                    };

                    var imageFiles = Directory.GetFiles(subdirectory)
                        .Where(filePath => filePath.EndsWith(".jpg") || filePath.EndsWith(".png"));

                    foreach (var imageFile in imageFiles)
                    {
                        var imagePath = imageFile.Substring(_webHostEnvironment.WebRootPath.Length).Replace('\\', '/');
                        folderNode.children.Add(new FancyTreeNode
                        {
                            title = Path.GetFileNameWithoutExtension(imageFile),
                            path = "/TESTING" + imagePath,
                            key = "/TESTING" + imagePath
                        });

                    }

                    fancyTreeNodes.Add(folderNode);
                }
            }
            else
            {
                fancyTreeNodes.Add(new FancyTreeNode { title = "Dossier introuvable", folder = true , key = "NotFound" });
            }

            return fancyTreeNodes;
        }

        [HttpGet]
        public IActionResult GetImages(string folderPath)
        {
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath.TrimStart('/'));
            var images = new List<object>();

            if (Directory.Exists(fullPath))
            {
                var imageFiles = Directory.GetFiles(fullPath)
                    .Where(filePath => filePath.EndsWith(".jpg") || filePath.EndsWith(".png"))
                    .Select(filePath => new { path = $"{folderPath}/{Path.GetFileName(filePath)}" });

                images.AddRange(imageFiles);
            }

            return Json(images);
        }

    }
}
