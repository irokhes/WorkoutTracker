using System;
using System.Collections.Generic;

namespace WorkoutTracker.Api.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}