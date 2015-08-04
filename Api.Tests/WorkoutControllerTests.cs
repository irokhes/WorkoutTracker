using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Machine.Fakes;
using Machine.Specifications;
using Moq;
using WorkoutTracker.Api;
using WorkoutTracker.Api.Controllers;
using WorkoutTracker.Api.Dtos;
using WorkoutTracker.Api.Models;
using It = Machine.Specifications.It;

namespace UnitTests
{
    public class When_getting_the_list_of_workouts : WithSubject<WorkoutController>
    {
        static List<Workout> workouts;
        static List<WorkoutDto> result;

        Establish context = () =>
        {
            workouts = new List<Workout>
            {
                new Workout{ Name = "first workout", Date = DateTime.Now.AddDays(-1).Date},
                new Workout{ Name = "second workout", Date = DateTime.Now.Date}
            };

            var mockRepository = new Mock<IRepository<Workout>>();
            mockRepository.Setup(x => x.GetAll()).Returns(workouts);
            The<IUnitOfWork>().WhenToldTo(x => x.RepositoryFor<Workout>()).Return(mockRepository.Object);
        };

        Because of = () =>
        {
            result = (List<WorkoutDto>)Subject.Get();
        };

        It should_contain_a_list_of_workouts = () =>
        {
            result.Count.ShouldEqual(2);
        };
    }

    public class When_creating_a_new_workout : WithSubject<WorkoutController>
    {
        static List<Workout> workouts;
        static IHttpActionResult result;

        Establish context = () =>
        {
            var mockRepository = new Mock<IRepository<Workout>>();
            Subject.Request = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://newWorkoutUri")
            };
            The<IUnitOfWork>().WhenToldTo(x => x.RepositoryFor<Workout>()).Return(mockRepository.Object);
        };

        Because of = () =>
        {
            result = Subject.Post(new WorkoutDto { Name = "first workout", Date = DateTime.Now.AddDays(-1).Date });
        };

        It should_contain_a_list_of_workouts = () =>
        {
            result.ShouldBeOfExactType<CreatedNegotiatedContentResult<WorkoutDto>>();
            ((CreatedNegotiatedContentResult<WorkoutDto>)result).Content.Name.ShouldEqual("first workout");
        };
    }

    public class when_adding_an_exercise_to_an_existing_workout : WithSubject<WorkoutController>
    {
        static List<Workout> workouts;
        static IHttpActionResult result;
        static Workout workout;
        Establish context = () =>
        {

            workout = new Workout { Id = 1, Name = "first workout", Date = DateTime.Now.AddDays(-1).Date, Exercises = new List<WorkoutExercise>() };


            var mockRepository = new Mock<IRepository<Workout>>();
            mockRepository.Setup(x => x.GetById(workout.Id)).Returns(workout);
            The<IUnitOfWork>().WhenToldTo(x => x.RepositoryFor<Workout>()).Return(mockRepository.Object);
        };

        Because of = () =>
        {

            result = Subject.Update(1, new WorkoutDto
            {
                Id = 1,
                Name = "first workout",
                Date = DateTime.Now.AddDays(-1).Date

            });
        };
        It should_contain_a_list_with_the_new_exercise = () =>
        {
            ((NegotiatedContentResult<Workout>)result).Content.Exercises.Count.ShouldEqual(1);
        };
    }
}
