using _2C2P.Services;
using _2C2P_CodeChallenge.Models;
using _2C2P_CodeChallenge.ViewModels;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml;
using System.Xml.XPath;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace _2C2P_CodeChallenge.Controllers.Api
{
    /// <summary>
    /// User related operations
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/transaction")]
    public class TransactionController : ApiController
    {
        private const string xmlContentType = "application/xml";
        private const string csvContentType = "text/csv";
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ITransactionService _service;
        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        public string Get()
        {
            var test = _service.GetTransactions();
            return null;
        }

        /// <summary>
        /// Queries transactions
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        [HttpGet]
        [ResponseType(typeof(object))]
        [Route("")]
        public async Task<IHttpActionResult> GetTransactions()
        {
            var test = await _service.GetTransactions();
            return Ok(test);
        }

        /// <summary>
        /// Queries transactions
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("xml")]
        public async Task<IHttpActionResult> SubmitXmlFile()
        {
            try
            {
                var transactions = new List<TransactionViewModel>();
                var resultContent = await Request.Content.ReadAsStreamAsync();

                if (resultContent.Length > 1024)
                    return BadRequest("File size it more than 1MB");

                XPathDocument doc = new XPathDocument(resultContent);
                XPathNavigator navigator = doc.CreateNavigator();
                XPathNodeIterator nodes = navigator.Select("/Transactions//Transaction");

                foreach (XPathNavigator item in nodes)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(item.OuterXml);
                    XmlElement xe = xmlDoc.DocumentElement;

                    var id = xe.GetAttribute("id");
                    var date = xe.ChildNodes[0].InnerText;
                    var amount = xe.ChildNodes[1].ChildNodes[0].InnerText;
                    var code = xe.ChildNodes[1].ChildNodes[1].InnerText;
                    var status = xe.ChildNodes[2].InnerText;

                    var transaction = new TransactionViewModel()
                    {
                        TransactionId = id,
                        TransactionDate = Convert.ToDateTime(date),
                        CurrencyCode = code,
                        Amount = GetAmount(amount),
                        Status = status
                    };

                    transactions.Add(transaction);
                }

                var failedTransactions = transactions.Where(t => !IsValidTransaction(t)).ToList();

                if (failedTransactions == null || failedTransactions.Count() == 0)
                {
                    await _service.SaveTransactions(transactions);
                    return Ok();
                }
                else
                {
                    LogFailedTransactions(failedTransactions);
                    return BadRequest("Some transactions failed to be validated");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        private void LogFailedTransactions(List<TransactionViewModel> transactions)
        {
            logger.Error("Transactions failed. object:{0}", transactions.ToJson());
        }

        private bool IsValidTransaction(TransactionViewModel transaction)
        {
            bool valid = true;

            if (string.IsNullOrEmpty(transaction.TransactionId) || transaction.TransactionId.Length > 50)
                valid = false;
            if (transaction.Amount <= 0)
                valid = false;
            if (string.IsNullOrEmpty(transaction.CurrencyCode) || transaction.CurrencyCode.Length > 3)
                valid = false;
            if (!Enum.TryParse(transaction.Status, out TransactionStatus importance))
                valid = false;
            return valid;
        }


        private TransactionStatus GetStatus(string value)
        {
            return (TransactionStatus)System.Enum.Parse(typeof(TransactionStatus), value);
        }

        private decimal GetAmount(string value)
        {
            if (decimal.TryParse(value, out decimal newDecimal))
                return newDecimal;
            else
                throw new FormatException("invalid amount type");
        }

        /// <summary>
        /// Queries transactions
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [ResponseType(typeof(object))]
        [Route("csv")]
        public async Task<IHttpActionResult> SubmitCsvFile()
        {
            try
            {
                var resultContent = await Request.Content.ReadAsStreamAsync();

                if (Request.Content.Headers.ContentType.MediaType == xmlContentType)
                {
                    XPathDocument doc = new XPathDocument(resultContent);

                    XPathNavigator navigator = doc.CreateNavigator();
                    XPathNodeIterator nodes = navigator.Select("/transactions");

                    foreach (XPathNavigator item in nodes)
                    {
                        Console.WriteLine(item.Value);
                    }
                }


                if (Request.Content.Headers.ContentType.MediaType == csvContentType)
                {

                }

                return Ok("ok");
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}