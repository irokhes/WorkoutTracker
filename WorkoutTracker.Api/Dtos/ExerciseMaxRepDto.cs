﻿using System;

namespace WorkoutTracker.Api.Dtos
{
    public class ExerciseMaxRepDto
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; }
        public int ExerciseId { get; set; }
        public int Weight { get; set; }
        public DateTime Date { get; set; }
    }
}