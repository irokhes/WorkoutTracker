using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using NUnit.Framework;
using WorkoutTracker.Api.Models;

namespace WorkoutTracker.Test.Integration
{
    class TestInitializer : DropCreateDatabaseAlways<WorkoutTrackerContext>
    {
        protected override void Seed(WorkoutTrackerContext context)
        {
           context.Exercises.AddOrUpdate(
                                c => c.Id,
                                new Exercise { Name = "Pull Ups", MuscularGroup = MuscularGroup.Back},
                                new Exercise { Name = "Bench Press", MuscularGroup = MuscularGroup.Chest});
 
            base.Seed(context);
        }
    }

    [SetUpFixture]
    public class SetUpFixture
    {
        WorkoutTrackerContext context;
        [SetUp]
        public void SetUp()
        {
            Database.SetInitializer(new TestInitializer());

            context = new WorkoutTrackerContext();
            context.Database.Initialize(true);
        }

        [TearDown]
        public void TearDown()
        {
            Database.Delete(ConfigurationManager.ConnectionStrings["workoutConnectionString"].ConnectionString);
        }
    }


}
