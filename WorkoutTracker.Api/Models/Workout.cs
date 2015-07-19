using System;
using System.Collections.Generic;

namespace WorkoutTracker.Api.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public TimeSpan EndTime { get; set; }

        public WODType WODType { get; set; }
        public virtual IEnumerable<Exercise> Exercises { get; set; }
    }

    public enum WODType
    {
       AMRAP,
       EMOM, 
       AFAP, 
       PowerLifting
    }
}