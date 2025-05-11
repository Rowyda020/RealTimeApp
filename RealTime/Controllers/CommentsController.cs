using Microsoft.AspNetCore.Mvc;

namespace RealTime.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
