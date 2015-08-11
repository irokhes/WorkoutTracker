using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WorkoutTracker.Api.Models;

namespace WorkoutTracker.Api.Dtos
{
    public class WorkoutListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public WODType WODType { get; set; }
    }
}