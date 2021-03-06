using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiDemo.Controllers
{
    public class ValuesController : ApiController
    {
        //  static varialble of strings which contains list of strings
        static List<string> strings = new List<string>()
        {
           "value0", "value1", "value2"
        };
        // from these below CRUD riles we can update delete read and get the information from Fiddler or postman 
        // GET api/values
        public IEnumerable<string> Get()
        {
            return strings;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return strings[id];
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
            strings.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
            strings[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            strings.RemoveAt(id);
        }
    }
}
