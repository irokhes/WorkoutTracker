using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WorkoutTracker.Api.Controllers
{
    public class ExerciseController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Exercise> Get()
        {
            return new List<Exercise>
            {
                new Exercise{Name = "Bench Press with barebell"},
                new Exercise{Name = "Bench Press with dumbell"}
            };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class Exercise
    {
        public string Name { get; set; }
    }
}