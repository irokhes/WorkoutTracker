using AutoMapper;
using WorkoutTracker.Api.Dtos;
using WorkoutTracker.Api.Models;

namespace WorkoutTracker.Api
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Workout, WorkoutDto>();
            Mapper.CreateMap<WorkoutDto, Workout>()
            .AfterMap((s, d) =>
            {
                foreach (var c in d.Exercises)
                    c.WorkoutId = d.Id;
            });
    

            Mapper.CreateMap<Exercise, ExerciseDto>();
            Mapper.CreateMap<ExerciseDto, Exercise>();

            Mapper.CreateMap<WorkoutExercise, WorkoutExerciseDto>()
                .ForMember(dest =>dest.Name, opts =>opts.MapFrom(x =>x.Exercise.Name));
            Mapper.CreateMap<WorkoutExerciseDto, WorkoutExercise>();

            Mapper.CreateMap<ImageDto, Images>();
            Mapper.CreateMap<Images, ImageDto>()
                .ForMember(dest =>dest.Image, opts =>opts.MapFrom(x =>x.ImageBase64));
        }
    }
}