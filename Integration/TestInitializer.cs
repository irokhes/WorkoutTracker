using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using NUnit.Framework;
using WorkoutTracker.Api;
using WorkoutTracker.Api.Controllers;

namespace WorkoutTracker.Test.Integration
{
    class TestInitializer : DropCreateDatabaseAlways<WorkoutTrackerContext>
    {
        protected override void Seed(WorkoutTrackerContext context)
        {
           context.Exercises.AddOrUpdate(
                                c => c.Id,
                                new Exercise { Name = "Exercise 1" },
                                new Exercise { Name = "Exercise 2" });
 
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
