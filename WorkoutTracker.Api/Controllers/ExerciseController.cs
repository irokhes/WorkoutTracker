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
        readonly IUnitOfWork _unitOfWork;

        public ExerciseController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/<controller>
        public IEnumerable<Exercise> Get()
        {
            return _unitOfWork.RepositoryFor<Exercise>().GetAll();
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            return Ok(_unitOfWork.RepositoryFor<Exercise>().GetById(id));
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
        public int  Id { get; set; }
        public string Name { get; set; }
    }
}