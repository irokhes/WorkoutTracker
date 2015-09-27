using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WorkoutTracker.Api.Models
{
    public class WorkoutTrackerContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseMaxRep> ExerciseMaxRep { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }

        public WorkoutTrackerContext()
            : base(ConfigurationManager.ConnectionStrings["workoutConnectionString"].ConnectionString)
        {
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkoutExercise>()
                        .HasKey(cp => new { cp.WorkoutId, cp.ExerciseId });


            modelBuilder.Entity<Exercise>()
                .HasMany(x => x.WorkoutsExercises)
                .WithRequired(y => y.Exercise)
                .HasForeignKey(x => x.ExerciseId);

            modelBuilder.Entity<Workout>()
                .HasMany(x => x.Exercises)
                .WithRequired(y => y.Workout)
                .HasForeignKey(x => x.WorkoutId);
        } 

        
    }
}