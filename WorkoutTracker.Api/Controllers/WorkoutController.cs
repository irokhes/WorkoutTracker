using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
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
            return Mapper.Map<List<Workout>, List<WorkoutDto>>(_unitOfWork.RepositoryFor<Workout>().GetAll().ToList());
        }

        

        [Route("api/workout/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var workout = _unitOfWork.RepositoryFor<Workout>().GetById(id);
            var workoutDto = Mapper.Map<Workout, WorkoutDto>(workout);
            return Ok(workoutDto);
        }

        [Route("api/workout")]
        [HttpPost]
        public IHttpActionResult Post(WorkoutDto newWorkout)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.RepositoryFor<Workout>().Insert(Mapper.Map<WorkoutDto, Workout>(newWorkout));
            _unitOfWork.Commit();
            return Created(Request.RequestUri + newWorkout.Id.ToString(CultureInfo.InvariantCulture), newWorkout);
    
        }


        [Route("api/workout/{id:int}")]
        [HttpPut]
        public IHttpActionResult Update(int id, WorkoutDto workout)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var oldEntity = _unitOfWork.RepositoryFor<Workout>().GetById(id);
            Mapper.Map<WorkoutDto, Workout>(workout,oldEntity);
            _unitOfWork.Commit();
            return Content(HttpStatusCode.Accepted, workout);
        }

        [Route("api/workout/postStuff")]
        public async Task<HttpResponseMessage> PostStuff()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = HttpContext.Current.Server.MapPath("~/App_Data/Temp/FileUploads");
            Directory.CreateDirectory(root);
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            if (result.FormData["workout"] == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var model = result.FormData["workout"];
            //TODO: Do something with the json model which is currently a string



            //get the files
            foreach (var file in result.FileData)
            {
                //TODO: Do something with each uploaded file
            }

            return Request.CreateResponse(HttpStatusCode.OK, "success!");
        }
    }
}