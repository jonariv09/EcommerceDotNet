using Microsoft.AspNetCore.Mvc;

namespace EcommercePlatform.Controllers
{
    public class CategoriasController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}