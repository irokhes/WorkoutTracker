using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WorkoutTracker.Api.Models
{
    public class WorkoutExercise
    {
        public virtual int WorkoutId { get; set; }
        public virtual int ExerciseId { get; set; }
        public virtual Workout Workout { get; set; }
        public virtual Exercise Exercise { get; set; }
        public virtual int NumReps { get; set; }
        public virtual decimal WeightOrDistance { get; set; }
    }
}