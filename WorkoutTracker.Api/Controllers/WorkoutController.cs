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
using Newtonsoft.Json;
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
            SaveWorkout(id, workout);
            return Content(HttpStatusCode.Accepted, workout);
        }

        void SaveWorkout(int id, WorkoutDto workout)
        {
            try
            {
                var oldEntity = _unitOfWork.RepositoryFor<Workout>().GetById(id);
                Mapper.Map<WorkoutDto, Workout>(workout, oldEntity);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }

        [Route("api/workout/upsert")]
        public async Task<HttpResponseMessage> SaveOrUpdate()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var temp = HttpContext.Current.Server.MapPath("~/App_Data/Temp/FileUploads");
            var root = HttpContext.Current.Server.MapPath("~/App_Data/Images/");
            Directory.CreateDirectory(temp);
            Directory.CreateDirectory(root);
            var provider = new MultipartFormDataStreamProvider(temp);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            if (result.FormData["workout"] == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }


            var jsonWorkout = result.FormData["workout"];
            //TODO: Do something with the json model which is currently a string
            var workoutDto = JsonConvert.DeserializeObject<WorkoutDto>(jsonWorkout);

            var id = result.FormData["id"];
            //get the files
            foreach (MultipartFileData file in result.FileData)
            {
                var image = new ImageDto();

                //Add the original extention to the file
                string imageName = Guid.NewGuid() + Path.GetExtension(file.Headers.ContentDisposition.FileName.Replace("\"", ""));
                string newFileName = String.Format("{0}{1}", root, imageName);
                File.Move(file.LocalFileName, newFileName);
                image.Name = imageName;
                workoutDto.Images.Add(image);
            }

            
            SaveWorkout(int.Parse(id), workoutDto);

            

            return Request.CreateResponse(HttpStatusCode.OK, "success!");
        }
    }
}