namespace WorkoutTracker.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMuscularGroupFromExercise : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Exercises", "MuscularGroup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exercises", "MuscularGroup", c => c.Int(nullable: false));
        }
    }
}
