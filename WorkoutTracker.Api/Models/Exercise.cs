using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WorkoutTracker.Api.Models
{
    public class Exercise
    {
        public int  Id { get;  set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<WorkoutExercise> WorkoutsExercises { get; set; }

    }

    public enum Split
    {
        Pull,
        Push,
        Legs,
    }
}