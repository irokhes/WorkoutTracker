using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using AutoMapper;
using WorkoutTracker.Api.Dtos;
using WorkoutTracker.Api.Models;

namespace WorkoutTracker.Api.Controllers
{
    public class ExerciseController : ApiController
    {
        readonly IUnitOfWork _unitOfWork;

        
        public ExerciseController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        static ExerciseDto GetDto(Exercise exercise)
        {
            return new ExerciseDto { Id = exercise.Id, Name = exercise.Name, Description = exercise.Description };
        }

        [Route("api/exercise")]
        public IEnumerable<ExerciseDto> Get()
        {
            return _unitOfWork.RepositoryFor<Exercise>().GetAll().Select(GetDto);
        }



        [Route("api/exercise/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(GetDto(_unitOfWork.RepositoryFor<Exercise>().GetById(id)));
        }

        [HttpPost]
        [Route("api/exercise")]
        public IHttpActionResult Post(ExerciseDto exerciseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Exercise newExercise = Mapper.Map<ExerciseDto, Exercise>(exerciseDto);
            _unitOfWork.RepositoryFor<Exercise>().Insert(newExercise);
            _unitOfWork.Commit();
            return Created(Request.RequestUri + newExercise.Id.ToString(CultureInfo.InvariantCulture), newExercise);   
        }

        [HttpPut]
        [Route("api/exercise/{id:int}")]
        public IHttpActionResult Put(int id, ExerciseDto exerciseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var oldExercise = _unitOfWork.RepositoryFor<Exercise>().GetById(id);
            var exercise = Mapper.Map<ExerciseDto, Exercise>(exerciseDto,oldExercise);
            _unitOfWork.Commit();

            return Content(HttpStatusCode.Accepted, exerciseDto);

        }

        [HttpDelete]
        [Route("api/exercise/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var exercise = _unitOfWork.RepositoryFor<Exercise>().GetById(id);
            if (exercise == null)
            {
                return NotFound();
            }
            _unitOfWork.RepositoryFor<Exercise>().Delete(id);
            _unitOfWork.Commit();

            return Ok();
        }
    }
}