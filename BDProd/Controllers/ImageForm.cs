using BDProd.Data;
using Microsoft.AspNetCore.Mvc;

namespace BDProd.Controllers
{
    public class ImageForm : Controller
    {

        private readonly BDProdContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageForm(BDProdContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LabSearch(string term)
        {
            try
            {
                var labs = _context.RefLabos
                .Where(l => l.RLAB_NOM.Contains(term))
                .Select(l => new { label = l.RLAB_NOM, value = l.RLAB_ID })
                .ToList();

                return Json(labs);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Erreur: " + ex.Message;
                return Json(new { error = ViewBag.ErrorMessage });
            }
        }

        public IActionResult ProdSearch(string term, string labSearchTerm)
        {
            try
            {
                int? labId = _context.RefLabos
                .Where(l => l.RLAB_NOM == labSearchTerm)
                .Select(l => l.RLAB_ID)
                .FirstOrDefault();

                var prods = _context.RefProds
                    .Where(p => (p.REF_CODE13.Contains(term) || p.REF_NOM.Contains(term)) && (labId == null || p.REF_LABIDMAJ == labId))
                    .Select(p => new { nomLong = p.REF_NOM, code13 = p.REF_CODE13 })
                    .ToList();

                return Json(prods);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Erreur: " + ex.Message;
                return Json(new { error = ViewBag.ErrorMessage });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, string folderPath)
        {
            folderPath = Path.Combine("TESTING", folderPath);
            folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    throw new Exception("The laboratory folder does not exist");
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                Console.WriteLine(uniqueFileName);
                var filePath = Path.Combine(folderPath, uniqueFileName);
                Console.WriteLine(filePath);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while uploading file: " + ex.Message);
            }
        }
    }

}
