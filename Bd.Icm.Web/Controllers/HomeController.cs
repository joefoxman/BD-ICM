using System.Web.Mvc;

namespace Bd.Icm.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}