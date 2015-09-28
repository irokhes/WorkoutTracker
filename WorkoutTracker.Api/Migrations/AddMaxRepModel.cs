namespace WorkoutTracker.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaxRepModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExerciseMaxReps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExerciseId = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.ExerciseId, cascadeDelete: true)
                .Index(t => t.ExerciseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExerciseMaxReps", "ExerciseId", "dbo.Exercises");
            DropIndex("dbo.ExerciseMaxReps", new[] { "ExerciseId" });
            DropTable("dbo.ExerciseMaxReps");
        }
    }
}
