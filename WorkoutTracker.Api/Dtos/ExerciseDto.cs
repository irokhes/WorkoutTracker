using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WorkoutTracker.Api.Models;

namespace WorkoutTracker.Api.Dtos
{
    public class ExerciseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int NumReps { get; set; }

    }
}