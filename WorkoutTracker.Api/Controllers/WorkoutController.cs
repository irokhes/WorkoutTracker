using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
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
            _unitOfWork.Commit();
        }

        [Route("api/workout/upsert")]
        public async Task<HttpResponseMessage> SaveOrUpdate()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            try
            {


                var temp = HostingEnvironment.MapPath("~/temp/images/");
                var root = HostingEnvironment.MapPath("~/images/");
                Directory.CreateDirectory(temp);
                Directory.CreateDirectory(root);
                var provider = new MultipartFormDataStreamProvider(temp);
                var result = await Request.Content.ReadAsMultipartAsync(provider);
                if (result.FormData["workout"] == null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }


                var jsonWorkout = result.FormData["workout"];
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
                    // Load image.
                    using (Image imageThumbnail = Image.FromFile(newFileName))
                    {
                        // Compute thumbnail size.
                        Size thumbnailSize = GetThumbnailSize(imageThumbnail);
                        // Get thumbnail.
                        using (Image thumbnail = imageThumbnail.GetThumbnailImage(thumbnailSize.Width,
                            thumbnailSize.Height, null, IntPtr.Zero))
                        {
                           
                            string imageThumbanilName = "thumbnail_" + imageName;

                            thumbnail.Save(String.Format("{0}{1}", root, imageThumbanilName));
                            image.Name = imageName;
                            image.Thumbnail = imageThumbanilName;

                            workoutDto.Images.Add(image);
                        }
                    }
                }
                SaveWorkout(int.Parse(id), workoutDto);
                return Request.CreateResponse(HttpStatusCode.OK, "success!");

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        Size GetThumbnailSize(Image original)
        {
            // Maximum size of any dimension.
            const int maxPixels = 80;

            // Width and height.
            int originalWidth = original.Width;
            int originalHeight = original.Height;

            // Compute best factor to scale entire image based on larger dimension.
            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double)maxPixels / originalWidth;
            }
            else
            {
                factor = (double)maxPixels / originalHeight;
            }

            // Return thumbnail size.
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }
    }
}