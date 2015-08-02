using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WorkoutTracker.Api.Models;

namespace WorkoutTracker.Api.Dtos
{
    public class WorkoutDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public DateTime Date { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public WODType WODType { get; set; }

        public List<WorkoutExerciseDto> Exercises { get; set; }
    }

    public class WorkoutExerciseDto
    {
        public int ExerciseId { get; set; }
        public  string Name { get; set; }
        public  int NumReps { get; set; }
        public  decimal WeightOrDistance { get; set; }
    }
}