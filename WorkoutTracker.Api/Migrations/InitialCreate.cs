namespace WorkoutTracker.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        MuscularGroup = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkoutExercises",
                c => new
                    {
                        WorkoutId = c.Int(nullable: false),
                        ExerciseId = c.Int(nullable: false),
                        NumReps = c.Int(nullable: false),
                        WeightOrDistance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.WorkoutId, t.ExerciseId })
                .ForeignKey("dbo.Workouts", t => t.WorkoutId, cascadeDelete: true)
                .ForeignKey("dbo.Exercises", t => t.ExerciseId, cascadeDelete: true)
                .Index(t => t.WorkoutId)
                .Index(t => t.ExerciseId);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                        WODType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Thumbnail = c.String(),
                        Workout_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workouts", t => t.Workout_Id)
                .Index(t => t.Workout_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkoutExercises", "ExerciseId", "dbo.Exercises");
            DropForeignKey("dbo.Images", "Workout_Id", "dbo.Workouts");
            DropForeignKey("dbo.WorkoutExercises", "WorkoutId", "dbo.Workouts");
            DropIndex("dbo.Images", new[] { "Workout_Id" });
            DropIndex("dbo.WorkoutExercises", new[] { "ExerciseId" });
            DropIndex("dbo.WorkoutExercises", new[] { "WorkoutId" });
            DropTable("dbo.Images");
            DropTable("dbo.Workouts");
            DropTable("dbo.WorkoutExercises");
            DropTable("dbo.Exercises");
        }
    }
}
