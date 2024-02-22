using BDProd.Data;
using Microsoft.AspNetCore.Mvc;

namespace BDProd.Controllers
{
    public class ImageForm : Controller
    {

        public IActionResult Index(string selectedImages)
        {
            string[] images = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(selectedImages);
            return View(images);
        }

        private readonly BDProdContext _context;

        public ImageForm(BDProdContext context)
        {
            _context = context;
        }

        public IActionResult LabSearch(string term)
        {
            var labs = _context.RefLabos
                .Where(l => l.RLAB_NOM.Contains(term))
                .Select(l => new { label = l.RLAB_NOM, value = l.RLAB_ID })
                .ToList();

            return Json(labs);
        }

        public IActionResult ProdSearch(string term)
        {
            var prods = _context.RefProds
                .Where(p => p.REF_CODE13.Contains(term) || p.REF_NOM.Contains(term))
                .Select(p => new { nomLong = p.REF_NOM, code13 = p.REF_CODE13 })
                .ToList();

            return Json(prods);
        }

    }

}
