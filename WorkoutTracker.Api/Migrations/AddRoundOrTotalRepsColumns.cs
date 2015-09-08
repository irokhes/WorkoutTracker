namespace WorkoutTracker.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoundOrTotalRepsColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workouts", "RoundsOrTotalReps", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workouts", "RoundsOrTotalReps");
        }
    }
}
