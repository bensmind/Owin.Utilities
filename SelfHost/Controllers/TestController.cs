using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinSamples.SelfHost.Controllers
{
    public class TestController : ApiController
    {
        // GET api/test
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/test/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/test
        public IHttpActionResult Post([FromBody]string value)
        {
            return Created(Url.Link("Api", new {controller = "test", id="5"}), value);
        }

        // PUT api/test/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            return Ok();
        }

        // DELETE api/test/5
        public void Delete(int id)
        {
            //returns 204 No Content see: http://stackoverflow.com/a/2342589/2884192
        }
    }
}
