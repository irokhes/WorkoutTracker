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
                                     Name = "Wall Balls",
                                     Description = " Is a compound exercise which combines a front squat with a medicine ball and a push press-like throwing of the ball to a target located some distance above the exerciser.",
                                 },
                                 new Exercise
                                 {
                                     Name = "Wall Walks",
                                     Description = "Chest on the floor with feet against the wall. Press body off of floor. Simultaneously walk hands toward the wall while walking feet up the wall. Come to a reverse handstand position with feet still against wall. Return to the floor by simultaneously walking feet down the wall and hands away from the wall. Return chest to floor.",
                                 },
                                 new Exercise
                                 {
                                     Name = "Squat Clean",
                                     Description = @"Deadlift the barbell until it reaches the height of your upper thighs. Without stopping the upward momentum of the bar, violently jump and shrug upwards while simultaneously pulling yourself down. Receive the weight at your shoulders. Complete the movement by standing fully with the barbell at shoulder height. Safe/efficient technique requires the hips to fully extend, while the arms are locked out, upon jumping/shrugging, and the use of a full front \'rack\' position when receiving the weight. A \'squat\' clean refers to the height of the receiving position. The crease of the hips must be below the height of the knees at the bottom of the squat. Ideally, the athlete receives the barbell immediately in the \'squat\' position, versus catching it high and riding it down to the bottom.",
                                 },
                                 new Exercise
                                 {
                                     Name = "Broad Jump",
                                     Description = "",
                                 },
                                 new Exercise
                                 {
                                     Name = "Pull Ups",
                                     Description = "",
                                 },
                                 new Exercise
                                 {
                                     Name = "Lunges",
                                     Description = "",
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
