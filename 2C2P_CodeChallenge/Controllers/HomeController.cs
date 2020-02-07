using _2C2P.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace _2C2P_CodeChallenge.Controllers
{
    public class HomeController : Controller
    {
        private ITransactionService _service;

        public HomeController(ITransactionService service)
        {
            _service = service;
        }

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

        public async Task<ActionResult> QueryTransactions()
        {
            var transactions = await _service.GetTransactions();
            ViewBag.Message = "Your transactions page.";

            return View(transactions);
        }
    }
}