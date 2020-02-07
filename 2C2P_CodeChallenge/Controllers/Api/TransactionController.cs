using _2C2P.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace _2C2P_CodeChallenge.Controllers.Api
{
    /// <summary>
    /// User related operations
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/transaction")]
    public class TransactionController : ApiController
    {
        private ITransactionService _service;
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
        public IHttpActionResult GetTransactions()
        {
          var test = _service.GetTransactions();
          return Ok(test);
        }
    }
}