using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using WorkoutTracker.Api;
using WorkoutTracker.Api.Models;

namespace WorkoutTracker.Test.Integration
{
    [TestFixture]
    class DbPlayground
    {
        UnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            var context = new WorkoutTrackerContext();
            _unitOfWork = new UnitOfWork(context);
        }

        [Test]
        public void getting_all_exercises()
        {
            var result = _unitOfWork.RepositoryFor<Exercise>().GetAll();

           Assert.Greater(result.Count(), 0);
        }

        [Test]
        public void creating_an_exercise()
        {
            var newExercise = new Exercise
            {
                Name = "Power Clean",
                Description = "The overhead squat is a deep squat performed while holding a barbell over your head with straight arms. It actually feels more like a snatch (one of the Olympic lifts) than a back squat."
            };
            _unitOfWork.RepositoryFor<Exercise>().Insert(newExercise);
            _unitOfWork.Commit();
            var result =_unitOfWork.RepositoryFor<Exercise>().GetById(newExercise.Id);
            Assert.IsNotNull(result);
        }

        [Test]
        public void creating_a_new_workout()
        {
            var exercisesInDB = _unitOfWork.RepositoryFor<Exercise>().GetAll();
            var workout = new Workout { 
                Date = DateTime.Now, 
                Name = "Holleyman 2.0", 
                WODType = WODType.AFAP,  
                Exercises = exercisesInDB
                .Select(x => new WorkoutExercise{ Exercise = x, NumReps = 3, WeightOrDistance = 20}).ToList()};
            var workout2 = new Workout
            {
                Date = DateTime.Now,
                Name = "Wod 22-7-15",
                WODType = WODType.AMRAP,
                Exercises = exercisesInDB
                    .Select(x => new WorkoutExercise { Exercise = x, NumReps = 5, WeightOrDistance = 10 }).ToList()
            };
            _unitOfWork.RepositoryFor<Workout>().Insert(workout);
            _unitOfWork.RepositoryFor<Workout>().Insert(workout2);

            _unitOfWork.Commit();
            var result = _unitOfWork.RepositoryFor<Workout>().GetById(workout.Id);
            Assert.Greater(result.Exercises.Count, 0);
            var result2 = _unitOfWork.RepositoryFor<Workout>().GetById(workout2.Id);
            Assert.Greater(result2.Exercises.Count, 0);

        }

    }
}
