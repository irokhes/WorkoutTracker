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
                                new Exercise { Name = "Pull Ups", 
                                    Description = "The pull up is perhaps the best exercise you can do if you want to build a strong, lean upper body. All you need is a bar that will support your weight.", 
                                    MuscularGroup = MuscularGroup.Back },
                                new Exercise { Name = "Bench Press", 
                                    Description = "A lift in weightlifting that is executed from a usually horizontal position on a bench, in which the weight is lifted from the chest to arm's length and then lowered back to the chest.", 
                                    MuscularGroup = MuscularGroup.Chest });
 
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
