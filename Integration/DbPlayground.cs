using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WorkoutTracker.Api;
using WorkoutTracker.Api.Controllers;
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

            Assert.AreEqual(result.Count(), 2);
        }

        [Test]
        public void creating_an_exercise()
        {
            var newExercise = new Exercise {MuscularGroup = MuscularGroup.Back, Name = "Pull ups"};
            _unitOfWork.RepositoryFor<Exercise>().Insert(newExercise);
            _unitOfWork.Commit();
            var result =_unitOfWork.RepositoryFor<Exercise>().GetById(newExercise.Id);
            Assert.IsNotNull(result);
        }

        [Test]
        public void creating_a_new_workout()
        {
            var exercisesInDB = _unitOfWork.RepositoryFor<Exercise>().GetAll();
            var exercise = exercisesInDB.First();
            var exercises = new List<Exercise>{new Exercise{Id = exercise.Id, MuscularGroup = exercise.MuscularGroup, Name = exercise.Name}};
            var workout = new Workout {Date = DateTime.Now, Name = "Chest workout", Exercises = exercises};
            _unitOfWork.RepositoryFor<Workout>().Insert(workout);
            _unitOfWork.Commit();
            var result = _unitOfWork.RepositoryFor<Workout>().GetById(workout.Id);
            Assert.IsNotNull(result);
        }

    }
}
