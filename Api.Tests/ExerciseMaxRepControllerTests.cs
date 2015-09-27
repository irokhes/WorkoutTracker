using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Http.Routing;
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
    public class When_getting_a_list_of_maxp_rep_exercises : WithSubject<ExerciseMaxRepController>
    {
        static List<ExerciseMaxRep> maxReps;
        static IEnumerable<ExerciseMaxRepDto> result;
        Establish context = () =>
        {
            maxReps = new List<ExerciseMaxRep>
            {
                new ExerciseMaxRep{ Exercise = new Exercise{Name = "Squat Clean"}, Weight = 90, Date = DateTime.Today },
            };
            var mockRepository = new Mock<IRepository<ExerciseMaxRep>>();
            mockRepository.Setup(x => x.GetAll()).Returns(maxReps);
            The<IUnitOfWork>().WhenToldTo(x => x.RepositoryFor<ExerciseMaxRep>()).Return(mockRepository.Object);
        };

        Because of = () =>
        {
            result = Subject.Get();
        };

        It should_retur_a_list_of_max_rep_exercises = () =>
        {
            result.Count().ShouldEqual(1);
            result.First().ExerciseName.ShouldEqual("Squat Clean");
            result.First().Weight.ShouldEqual(90);
            result.First().Date.ShouldEqual(DateTime.Today);
        };


    }

    public class When_getting_a_max_rep_exercise_by_exercise_id : WithSubject<ExerciseMaxRepController>
    {
        static IHttpActionResult result;
        Establish context = () =>
        {
            
            var exerciseMaxRep = new ExerciseMaxRep{Date = DateTime.Today,Exercise = new Exercise{Name = "Snatch", Id = 1}, Weight = 60};
            The<IUnitOfWork>()
                .WhenToldTo(x => x.RepositoryFor<ExerciseMaxRep>()
                    .Get(Param.IsAny<Expression<Func<ExerciseMaxRep, bool>>>()))
                    .Return(new List<ExerciseMaxRep> { exerciseMaxRep });
        };

        Because of = () =>
        {
            result = Subject.Get(1);
        };

        It should_return_the_max_rep_for_the_exercise = () =>
        {
            result.ShouldBeOfExactType<OkNegotiatedContentResult<ExerciseMaxRepDto>>();
            var conNegResult = result as OkNegotiatedContentResult<ExerciseMaxRepDto>;
            conNegResult.Content.ShouldMatch(x =>x.ExerciseName == "Snatch");
        };
    }

    public class When_creating_a_max_rep_exercise : WithSubject<ExerciseMaxRepController>
    {
        static IHttpActionResult result;
        static Exercise exercise;
        Establish context = () =>
        {
            var request = An<HttpRequestMessage>();
            request.RequestUri = new Uri("http://www.contoso.com/");

            var route = An<IHttpRouteData>();

            var httpConfiguration = An<HttpConfiguration>();

            var controllerContext = new HttpControllerContext(httpConfiguration, route, request);

            Subject.ControllerContext = controllerContext;
            exercise = new Exercise{Id = 1, Name = "Squat"};
            The<IUnitOfWork>()
                .WhenToldTo(x => x.RepositoryFor<Exercise>()
                    .GetById(Param.IsAny<int>()))
                    .Return(exercise);

            The<IUnitOfWork>().WhenToldTo(x => x.RepositoryFor<ExerciseMaxRep>().Insert(Param.IsAny<ExerciseMaxRep>()));
        };

        Because of = () =>
        {
            result = Subject.Post(new ExerciseMaxRepDto {Date = DateTime.Today, Weight = 82, ExerciseName = "Squat", ExerciseId = 1});
        };

        It should_create_the_max_rep_entry_with_the_correct_values = () =>
        {
            The<IUnitOfWork>().WasToldTo(x => x.RepositoryFor<ExerciseMaxRep>().Insert(Param<ExerciseMaxRep>.Matches(y => y.Exercise == exercise)));
            The<IUnitOfWork>().WasToldTo(x => x.RepositoryFor<ExerciseMaxRep>().Insert(Param<ExerciseMaxRep>.Matches(y => y.Date == DateTime.Today)));
            The<IUnitOfWork>().WasToldTo(x => x.RepositoryFor<ExerciseMaxRep>().Insert(Param<ExerciseMaxRep>.Matches(y => y.Weight == 82)));
            
        };

        It should_return_created_response = () =>
        {
            result.ShouldBeOfExactType<CreatedNegotiatedContentResult<ExerciseMaxRepDto>>();
            var conNegResult = result as CreatedNegotiatedContentResult<ExerciseMaxRepDto>;
        };
    }
}
