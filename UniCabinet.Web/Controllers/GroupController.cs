using Microsoft.AspNetCore.Mvc;

namespace UniCabinet.Web.Controllers
{
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
