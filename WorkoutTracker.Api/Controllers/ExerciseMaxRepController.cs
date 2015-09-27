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
    public class ExerciseMaxRepController : ApiController
    {
        readonly IUnitOfWork _unitOfWork;

        public ExerciseMaxRepController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("api/maxRep")]
        public IEnumerable<ExerciseMaxRepDto> Get()
        {
            return _unitOfWork.RepositoryFor<ExerciseMaxRep>().GetAll().Select(x => new ExerciseMaxRepDto
            {
                ExerciseName = x.Exercise.Name,
                Date = x.Date,
                Weight = x.Weight
            });
        }

        [Route("api/maxRep/{int}")]
        public IHttpActionResult Get(int exerciseId)
        {
            var maxRep = _unitOfWork.RepositoryFor<ExerciseMaxRep>().Get(x => x.Exercise.Id == exerciseId).FirstOrDefault();
            if (maxRep == null)
                return NotFound();

            return Ok(new ExerciseMaxRepDto
            {
                ExerciseName = maxRep.Exercise.Name,
                Date = maxRep.Date,
                Weight = maxRep.Weight
            });
        }

        [HttpPost]
        [Route("api/maxRep/")]
        public IHttpActionResult Post(ExerciseMaxRepDto exerciseMaxRepDto)
        {
            var exercise = _unitOfWork.RepositoryFor<Exercise>().GetById(exerciseMaxRepDto.ExerciseId);
            if (exercise == null)
                return NotFound();

            var exerciseMaxRep = new ExerciseMaxRep
            {
                Exercise = exercise,
                Date = exerciseMaxRepDto.Date,
                Weight = exerciseMaxRepDto.Weight
            };

            _unitOfWork.RepositoryFor<ExerciseMaxRep>().Insert(exerciseMaxRep);
            _unitOfWork.Commit();
            exerciseMaxRepDto.ExerciseId = exerciseMaxRep.Id;
            return Created(Request.RequestUri + exerciseMaxRep.Id.ToString(CultureInfo.InvariantCulture),
                exerciseMaxRepDto);
        }
    }


}
