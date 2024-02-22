using System;
using System.IO;
using System.Linq;
using BDProd.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace BDProd.Controllers
{
    public class ImageSelection : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageSelection(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        //private const string OverPath = "C:\\Changes_C\\Projects\\TESTING";

        public IActionResult Index()
        {
            //var imageFolders = LoadImageFoldersFromServer(OverPath);
            var imageFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "TESTING");
            //var imageFolderPath = "C:\\Changes_C\\Projects\\TESTING";
            Console.WriteLine(imageFolderPath);
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
                    //var relativePath = subdirectory.Substring(OverPath.Length).Replace('\\', '/');
                    var folderNode = new FancyTreeNode
                    {
                        title = Path.GetFileName(subdirectory),
                        folder = true,
                        children = new List<FancyTreeNode>(),
                        path = relativePath, 
                        key = relativePath 
                    };

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
            //var fullPath = Path.Combine(OverPath, folderPath.TrimStart('/'));
            var images = new List<object>();

            if (Directory.Exists(fullPath))
            {
                var imageFiles = Directory.GetFiles(fullPath)
                    .Where(filePath => filePath.EndsWith(".jpg") || filePath.EndsWith(".png"))
                    .Select(filePath => new { path = $"{folderPath}/{Path.GetFileName(filePath)}" });
                Console.WriteLine(imageFiles);
                images.AddRange(imageFiles);
            }
            return Json(images);
        }

    }
}
