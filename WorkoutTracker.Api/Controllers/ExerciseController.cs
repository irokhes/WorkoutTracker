using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkoutTracker.Api.Models;

namespace WorkoutTracker.Api.Controllers
{
    [RoutePrefix("api/newExercise")]
    public class ExerciseController : ApiController
    {
        readonly IUnitOfWork _unitOfWork;

        
        public ExerciseController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        [Route("/")]
        public IEnumerable<Exercise> Get()
        {
            return _unitOfWork.RepositoryFor<Exercise>().GetAll();
        }

        [Route("/{id:int:min(1)}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(_unitOfWork.RepositoryFor<Exercise>().GetById(id));
        }

        [HttpPost]
        [Route("/")]
        public IHttpActionResult Post([FromBody]Exercise newExercise)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.RepositoryFor<Exercise>().Insert(newExercise);

            return Created(Request.RequestUri + newExercise.Id.ToString(CultureInfo.InvariantCulture), newExercise);
    
        }
    }
}