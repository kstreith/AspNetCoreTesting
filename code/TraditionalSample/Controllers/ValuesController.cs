using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MiddlewareSample.Exceptions;
using TraditionalSample.Models;

namespace TraditionalSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id == 1)
            {
                return "value";
            }
            throw new NotFoundException();
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] ValueModel value)
        {
            return CreatedAtAction(nameof(Get), new { id = 2 }, null);
        }
    }
}
