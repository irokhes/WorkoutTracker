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
    }
}
