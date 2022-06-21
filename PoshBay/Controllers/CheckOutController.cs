using Microsoft.AspNetCore.Mvc;

namespace PoshBay.Controllers
{
    public class CheckOutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
