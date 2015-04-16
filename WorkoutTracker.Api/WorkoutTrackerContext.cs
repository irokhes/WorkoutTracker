using System.Configuration;
using System.Data.Entity;
using WorkoutTracker.Api.Controllers;

namespace WorkoutTracker.Api
{
    public class WorkoutTrackerContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }

        public WorkoutTrackerContext()
            : base(ConfigurationManager.ConnectionStrings["workoutConnectionString"].ConnectionString)
        {
           
        }
    }
}