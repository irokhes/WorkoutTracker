namespace WorkoutTracker.Api.Models
{
    public class Exercise
    {
        public int  Id { get;  private set; }
        public string Name { get; set; }
        public MuscularGroup MuscularGroup { get; set; }
    }


    public enum MuscularGroup
    {
        Biceps,
        Triceps,
        Back,
        Chest,
        Shoulders,
        Legs
    }

    public enum Split
    {
        Pull,
        Push,
        Legs,
    }
}