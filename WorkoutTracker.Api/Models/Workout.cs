using System;
using System.Collections.Generic;

namespace WorkoutTracker.Api.Models
{
    public class Workout
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual TimeSpan Time { get; set; }

        public virtual WODType WODType { get; set; }
        public virtual ICollection<WorkoutExercise> Exercises { get; set; }

        public virtual ICollection<Images> Images { get; set; } 
    }

    public enum WODType
    {
       AMRAP,
       EMOM, 
       AFAP, 
       PowerLifting
    }
}