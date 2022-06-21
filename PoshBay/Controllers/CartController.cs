using Microsoft.AspNetCore.Mvc;

namespace PoshBay.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
