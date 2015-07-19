﻿using System;
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
    public class ExerciseController : ApiController
    {
        readonly IUnitOfWork _unitOfWork;

        
        public ExerciseController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        static ExerciseDto GetDto(Exercise exercise)
        {
            return new ExerciseDto { Id = exercise.Id, MuscularGroup = exercise.MuscularGroup, Name = exercise.Name };
        }

        public IEnumerable<ExerciseDto> Get()
        {
            return _unitOfWork.RepositoryFor<Exercise>().GetAll().Select(GetDto);
        }

        

        public IHttpActionResult Get(int id)
        {
            return Ok(GetDto(_unitOfWork.RepositoryFor<Exercise>().GetById(id)));
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Exercise newExercise)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _unitOfWork.RepositoryFor<Exercise>().Insert(newExercise);

            return Created(Request.RequestUri + newExercise.Id.ToString(CultureInfo.InvariantCulture), newExercise);
    
        }
    }
}