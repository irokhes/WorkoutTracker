using System;

namespace WorkoutTracker.Api.Models
{
    public class ExerciseMaxRep
    {
        public int Id { get; set; }
        public Exercise Exercise { get; set; }
        public int Weight { get; set; }
        public DateTime Date { get; set; }
    }
}