using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoggerClassLib;
using LoggerClassLib.Attributes;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    /// <summary>
    /// Represents a RESTful service of orders.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("0.9", Deprecated = true)]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        // GET api/values
        /// <summary>
        /// Get all Orders
        /// </summary>
        /// <returns>All available orders</returns>
        [HttpGet]
        [LogUsage("View Get API")]
        public ActionResult<IEnumerable<string>> Get()
        {

            ActivityLog<string>.Logger("We got here....");
            var res = new string[] { "value123333333333333", "value23333333333" };
            ActivityLog<string[]>.Logger("Result", res);
            return res;
        }

        // POST api/values
        /// <summary>
        /// Create a new Post
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Return new created Order</returns>
        [HttpPost]
        [MapToApiVersion("1.0")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}
