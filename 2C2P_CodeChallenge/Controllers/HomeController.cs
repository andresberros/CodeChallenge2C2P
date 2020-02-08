using _2C2P.Services;
using System;
using System.Linq;
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

        public async Task<ActionResult> QueryTransactions(string option, string search, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var transactions = await _service.GetTransactions();
            ViewBag.Message = "Your transactions page.";

            if (!string.IsNullOrWhiteSpace(option) && !string.IsNullOrWhiteSpace(search))
            {
                var selectedOption = option.Trim().ToLower();

                if (selectedOption.Equals("currency"))
                    return View(transactions.Where(t => t.CurrencyCode.Trim().ToLower().Equals(search.Trim().ToLower())).ToList());
                else if (selectedOption.Equals("status"))
                    return View(transactions.Where(t => t.Status.Trim().ToLower().Equals(search.Trim().ToLower())).ToList());
                else
                    return View(transactions);
            }

            if (dateFrom != null || dateTo != null)
            {
                if (dateFrom == null || dateTo == null)
                {
                    ViewBag.Message = "You need to select a date range.";
                    return View();
                }

                if (dateFrom > dateTo)
                {
                    ViewBag.Message = "Please select a valid range.";
                    return View();
                }

                var result = transactions.Where(t => t.TransactionDate > dateFrom.Value && t.TransactionDate < dateTo.Value.AddDays(1)).ToList();
                return View(result);
            }

            return View(transactions);
        }
    }
}