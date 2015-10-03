using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            return _unitOfWork.RepositoryFor<ExerciseMaxRep>().GetAll().ToList().Select(x => new ExerciseMaxRepDto
            {
                Id = x.Id,
                ExerciseName = x.Exercise.Name,
                ExerciseId = x.ExerciseId,
                Date = x.Date,
                Weight = x.Weight
            });
        }

        [HttpGet]
        [Route("api/maxRep/exercise/{exerciseId:int}")]
        public IHttpActionResult GetByExerciseId(int exerciseId)
        {
            var maxReps = _unitOfWork.RepositoryFor<ExerciseMaxRep>().Get(x => x.ExerciseId == exerciseId).ToList();

            return Ok(maxReps.Select(maxRep => new ExerciseMaxRepDto
            {
                Id = maxRep.Id,
                ExerciseName = maxRep.Exercise.Name,
                ExerciseId = maxRep.ExerciseId,
                Date = maxRep.Date,
                Weight = maxRep.Weight
            }));
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
