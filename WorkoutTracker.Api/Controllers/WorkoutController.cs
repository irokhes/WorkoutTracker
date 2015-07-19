using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkoutTracker.Api.Dtos;
using WorkoutTracker.Api.Models;

namespace WorkoutTracker.Api.Controllers
{
    public class WorkoutController : ApiController
    {
        readonly IUnitOfWork _unitOfWork;


        public WorkoutController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("api/workout")]
        public IEnumerable<WorkoutDto> Get()
        {
            return _unitOfWork.RepositoryFor<Workout>().GetAll().Select(GetDto);
        }

        WorkoutDto GetDto(Workout workout)
        {
            return new WorkoutDto
            {
                Id = workout.Id,
                Date = workout.Date,
                Name = workout.Name,
                WODType = workout.WODType
            };
        }

        [Route("api/workout/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(_unitOfWork.RepositoryFor<Workout>().GetById(id));
        }

        [Route("api/workout")]
        [HttpPost]
        public IHttpActionResult Post(Workout newWorkout)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.RepositoryFor<Workout>().Insert(newWorkout);
            
            return Created(Request.RequestUri + newWorkout.Id.ToString(), newWorkout);
    
        }


        [Route("api/workout/{id:int}")]
        [HttpPut]
        public IHttpActionResult Update(int id, Workout workout)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.RepositoryFor<Workout>().Update(workout);
            return Content(HttpStatusCode.Accepted, workout);
        }
    }
}