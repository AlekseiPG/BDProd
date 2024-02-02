using Microsoft.AspNetCore.Mvc;

namespace BDProd.Controllers
{
    public class ImageForm : Controller
    {

        public IActionResult Index(List<string> selectedImages)
        {
            string jsonSelectedImages = Newtonsoft.Json.JsonConvert.SerializeObject(selectedImages);

            return View((object)jsonSelectedImages);
        }

    }

}
