using WorkoutTracker.Api.Models;

namespace WorkoutTracker.Api.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<WorkoutTrackerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WorkoutTrackerContext context)
        {

            //context.Exercises.AddOrUpdate(
            //                     c => c.Id,
            //                     new Exercise
            //                     {
            //                         Name = "Wall Balls",
            //                         Description = "Is a compound exercise which combines a front squat with a medicine ball and a push press-like throwing of the ball to a target located some distance above the exerciser.",
            //                     },
            //                     new Exercise
            //                     {
            //                         Name = "Wall Walks",
            //                         Description = "Chest on the floor with feet against the wall. Press body off of floor. Simultaneously walk hands toward the wall while walking feet up the wall. Come to a reverse handstand position with feet still against wall. Return to the floor by simultaneously walking feet down the wall and hands away from the wall. Return chest to floor.",
            //                     },
            //                     new Exercise
            //                     {
            //                         Name = "Squat Clean",
            //                         Description = @"Deadlift the barbell until it reaches the height of your upper thighs. Without stopping the upward momentum of the bar, violently jump and shrug upwards while simultaneously pulling yourself down. Receive the weight at your shoulders. Complete the movement by standing fully with the barbell at shoulder height. Safe/efficient technique requires the hips to fully extend, while the arms are locked out, upon jumping/shrugging, and the use of a full front \'rack\' position when receiving the weight. A \'squat\' clean refers to the height of the receiving position. The crease of the hips must be below the height of the knees at the bottom of the squat. Ideally, the athlete receives the barbell immediately in the \'squat\' position, versus catching it high and riding it down to the bottom.",
            //                     },
            //                     new Exercise
            //                     {
            //                         Name = "Broad Jump",
            //                         Description = "The jumper stands at a line marked on the ground with the feet slightly apart. The athlete takes off and lands using both feet, swinging the arms and bending the knees to provide forward drive",
            //                     },
            //                     new Exercise
            //                     {
            //                         Name = "Pull Ups",
            //                         Description = "A pull-up is an upper-body compound pulling exercise. The most popular current meaning refers to a closed-chain bodyweight movement where the body is suspended by the arms, gripping something, and pulls up. As this happens, the wrists remain in neutral (straight, neither flexed nor extended) position, the elbows flex and the shoulder adducts and/or extends to bring the elbows to or sometimes behind the torso.",
            //                     },
            //                     new Exercise
            //                     {
            //                         Name = "Lunges",
            //                         Description = "A lunge can refer to any position of the human body where one leg is positioned forward with knee bent and foot flat on the ground while the other leg is positioned behind.",
            //                     },
            //                     new Exercise
            //                     {
            //                         Name = "Thruster",
            //                         Description = "The Thruster is a powerful movement that moves a weight through a large range of motion. This movement simply combines a front squat with a push press.",
            //                     },
            //                     new Exercise
            //                     {
            //                         Name = "Clean and Jerk",
            //                         Description = "The clean and jerk is a composite of two weightlifting movements: the clean and the jerk. During the clean, the lifter moves the barbell from the floor to a racked position across deltoids and clavicles. During the jerk the lifter raises the barbell to a stationary position above the head",
            //                     });
                                
        }
    }
}
