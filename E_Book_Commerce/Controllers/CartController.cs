using Microsoft.AspNetCore.Mvc;

namespace Book_Commerce.Controllers
{
    public class CartController : Controller
    {
        public IActionResult CartIndex()
        {
            return View();
        }
    }
}
