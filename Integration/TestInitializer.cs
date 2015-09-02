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
                                 new Exercise
                                 {
                                     Name = "Wall balls",
                                     Description = " Is a compound exercise which combines a front squat with a medicine ball and a push press-like throwing of the ball to a target located some distance above the exerciser.",
                                 },
                                 new Exercise
                                 {
                                     Name = "Thruster",
                                     Description = "The Thruster is a powerful movement that moves a weight through a large range of motion. This movement simply combines a front squat with a push press.",
                                 });

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
            Database.Delete(ConfigurationManager.ConnectionStrings["workoutConnectionString"].ConnectionString);
            Database.SetInitializer(new TestInitializer());
            context = new WorkoutTrackerContext();
            context.Database.Initialize(true);
        }

        [TearDown]
        public void TearDown()
        {

        }
    }


}
