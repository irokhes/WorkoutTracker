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
            return _unitOfWork.RepositoryFor<Workout>().GetAll().Select(DtoMapper.GetWorkoutDto);
        }

        

        [Route("api/workout/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            Workout workout = _unitOfWork.RepositoryFor<Workout>().GetById(id);
            WorkoutDto workoutDto = DtoMapper.GetWorkoutDto(workout);
            workoutDto.Exercises = workout.WorkoutExercises.Select(DtoMapper.GetWorkoutExerciseDto).ToList();
            return Ok(workoutDto);
        }

        [Route("api/workout")]
        [HttpPost]
        public IHttpActionResult Post(WorkoutDto newWorkout)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.RepositoryFor<Workout>().Insert(DtoMapper.GetWorkout(newWorkout));
            _unitOfWork.Commit();
            return Created(Request.RequestUri + newWorkout.Id.ToString(), newWorkout);
    
        }


        [Route("api/workout/{id:int}")]
        [HttpPut]
        public IHttpActionResult Update(int id, Workout workout)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.RepositoryFor<Workout>().Update(workout);
            _unitOfWork.Commit();
            return Content(HttpStatusCode.Accepted, workout);
        }
    }
}