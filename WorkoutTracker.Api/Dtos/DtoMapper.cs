using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutTracker.Api.Models;

namespace WorkoutTracker.Api.Dtos
{
    public class DtoMapper
    {
        public static WorkoutDto GetWorkoutDto(Workout workout)
        {
            return new WorkoutDto
            {
                Id = workout.Id,
                Name = workout.Name,
                Description = workout.Description,
                Date = workout.Date,
                WODType = workout.WODType
            };
        }

        public static WorkoutExerciseDto GetWorkoutExerciseDto(WorkoutExercise workoutExercise)
        {
            return new WorkoutExerciseDto
            {
                Name = workoutExercise.Exercise.Name,
                NumReps = workoutExercise.NumReps,
                WeightOrDistance = workoutExercise.WeightOrDistance
            };
        }
    }
}