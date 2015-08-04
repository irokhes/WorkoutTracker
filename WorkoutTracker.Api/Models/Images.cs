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

        [NotMapped]
        public byte[] Bytes
        {
            get
            {
                var filePath = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/App_Data/Images/{0}", Name));
                var fileStream = new FileStream(filePath, FileMode.Open);
                Image image = Image.FromStream(fileStream);
                var memoryStream = new MemoryStream();
                image.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            } 
        }
    }
}