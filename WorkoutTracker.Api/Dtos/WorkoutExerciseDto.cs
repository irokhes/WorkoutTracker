namespace WorkoutTracker.Api.Dtos
{
    public class WorkoutExerciseDto
    {
        public int ExerciseId { get; set; }
        public  string Name { get; set; }
        public  int NumReps { get; set; }
        public  decimal WeightOrDistance { get; set; }
    }
}