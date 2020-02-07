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

        public ActionResult UploadFile()
        {
            ViewBag.Message = "Upload a XML or CSV file.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}