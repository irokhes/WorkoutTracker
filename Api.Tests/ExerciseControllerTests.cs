using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Moq;
using WorkoutTracker.Api;
using WorkoutTracker.Api.Controllers;
using WorkoutTracker.Api.Models;
using It = Machine.Specifications.It;

namespace UnitTests
{
    public class When_getting_a_list_of_exercises : WithSubject<ExerciseController>
    {
        static List<Exercise> exercises;
        static IEnumerable<Exercise> result;
        Establish context = () =>
        {
            exercises = new List<Exercise>
            {
                new Exercise{ Name = "my first exercise"},
                new Exercise{ Name = "my second exercise"},
            };
            var mockRepository = new Mock<IRepository<Exercise>>();
            mockRepository.Setup(x => x.GetAll()).Returns(exercises);
            The<IUnitOfWork>().WhenToldTo(x =>x.RepositoryFor<Exercise>()).Return(mockRepository.Object);
        };

        Because of = () =>
        {
            result = Subject.Get();
        };

        It should_retur_a_list_of_exercises = () =>
        {
            result.Count().ShouldEqual(2);
        };


    }

}
