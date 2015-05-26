using System.Configuration;
using System.Data.Entity;

namespace WorkoutTracker.Api.Models
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