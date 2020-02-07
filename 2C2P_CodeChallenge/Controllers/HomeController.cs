using _2C2P.Services;
using System.Web.Mvc;

namespace _2C2P_CodeChallenge.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {             
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}