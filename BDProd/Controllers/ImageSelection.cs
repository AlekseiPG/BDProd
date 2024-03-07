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

        public IActionResult Index()
        {
            var imageFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "TESTING");
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
                fancyTreeNodes.Add(new FancyTreeNode { title = "Dossier introuvable", folder = true, key = "NotFound" });
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
                Console.WriteLine(imageFiles);
                images.AddRange(imageFiles);
            }
            return Json(images);
        }


        [HttpPost]
        public IActionResult MoveToTrash(List<string> images, string folderPath)
        {
            Console.WriteLine("Mouvement à la poubelle");
            try
            {
                Console.WriteLine(_webHostEnvironment.WebRootPath);
                Console.WriteLine(folderPath);
                folderPath = folderPath.Replace('/', '\\');
                Console.WriteLine(folderPath);
                string trashFolderPath = _webHostEnvironment.WebRootPath + folderPath;
                Console.WriteLine(trashFolderPath);
                trashFolderPath = Path.Combine(trashFolderPath, "Poubelle");
                Console.WriteLine(trashFolderPath);
                
                if (!Directory.Exists(trashFolderPath))
                {
                    Directory.CreateDirectory(trashFolderPath);
                    Console.WriteLine("Poubelle créée avec succès");
                }
                else
                {
                    Console.WriteLine("Poubelle existe déjà");
                }

                foreach (var imagePath in images)
                {
                    string imageName = Path.GetFileName(imagePath);
                    Console.WriteLine(imagePath);
                    string imagePathLocal = _webHostEnvironment.WebRootPath + imagePath;
                    Console.WriteLine(imagePathLocal);
                    string destinationPath = Path.Combine(trashFolderPath, imageName);
                    Console.WriteLine($"Déplacement de {imageName} vers {destinationPath}");
                    System.IO.File.Move(imagePathLocal, destinationPath);
                    Console.WriteLine("Succès?");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite lors du déplacement des images : {ex.Message}");
            }
        }

    }
}
