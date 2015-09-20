using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using AutoMapper;
using Newtonsoft.Json;
using WorkoutTracker.Api.Dtos;
using WorkoutTracker.Api.Models;
using WorkoutTracker.Api.Services;

namespace WorkoutTracker.Api.Controllers
{
    public class WorkoutController : ApiController
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IImageService _imageService;


        public WorkoutController(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }

        [Route("api/workout")]
        public IEnumerable<WorkoutListDto> Get()
        {
            return _unitOfWork.RepositoryFor<Workout>().GetAll()
                .Select(x => new WorkoutListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Date = x.Date,
                    Time = x.Time,
                    RoundsOrTotalReps = x.RoundsOrTotalReps,
                    WODType = x.WODType,

                }).ToList();
        }



        [Route("api/workout/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var workoutDto = Mapper.Map<Workout, WorkoutDto>(_unitOfWork.RepositoryFor<Workout>().GetById(id));
            return Ok(workoutDto);
        }

        [Route("api/workout")]
        [HttpPost]
        public IHttpActionResult Post(WorkoutDto newWorkout)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Workout workout = Mapper.Map<WorkoutDto, Workout>(newWorkout);
            _unitOfWork.RepositoryFor<Workout>().Insert(workout);
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

        void SaveWorkout(int id, WorkoutDto workoutDto)
        {
            var oldEntity = _unitOfWork.RepositoryFor<Workout>().GetById(id);
            var workout = Mapper.Map<WorkoutDto, Workout>(workoutDto, oldEntity);

            if (id == 0)
            {
                _unitOfWork.RepositoryFor<Workout>().Insert(workout);
            }
            
            _unitOfWork.Commit();
        }

        [Route("api/workout/upsert")]
        public async Task<HttpResponseMessage> SaveOrUpdate()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }



            var temp = HostingEnvironment.MapPath("~/temp/images/");
            if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);

            var provider = new MultipartFormDataStreamProvider(Path.GetTempPath());
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            if (result.FormData["workout"] == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }


            var jsonWorkout = result.FormData["workout"];
            var workoutDto = JsonConvert.DeserializeObject<WorkoutDto>(jsonWorkout);

            workoutDto.Images =_imageService.GetFiles(result.FileData).ToList();

            int getIdIfExistingWorkout = result.FormData["id"] == "undefined" ? 0 : int.Parse(result.FormData["id"]);
            var id = getIdIfExistingWorkout;

            SaveWorkout(id, workoutDto);
            return Request.CreateResponse(HttpStatusCode.OK, "success!");



        }
        [Route("api/workout/image/{id}")]
        [HttpGet]
        public IHttpActionResult GetImage(string id)
        {
            var fileStream = new FileStream(HostingEnvironment.MapPath("~/images/" + id), FileMode.Open);

            var resp = new HttpResponseMessage
            {
                Content = new StreamContent(fileStream)
            };

            // Find the MIME type
            //string mimeType = _extensions[Path.GetExtension(path)];
            string mimeType = "image/png";
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
            IHttpActionResult response = ResponseMessage(resp);
            return response;
        }

        
    }


}