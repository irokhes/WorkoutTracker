using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Web.Hosting;

namespace WorkoutTracker.Api.Models
{
    public class Images
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }

    }
}