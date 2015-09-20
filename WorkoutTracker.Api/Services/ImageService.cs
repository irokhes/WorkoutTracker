using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using WorkoutTracker.Api.Dtos;

namespace WorkoutTracker.Api.Services
{
    public interface IImageService
    {
        IEnumerable<ImageDto> GetFiles(Collection<MultipartFileData> fileData);
    }

    class ImageService : IImageService
    {
        public IEnumerable<ImageDto> GetFiles(Collection<MultipartFileData> files)
        {
            var images = new List<ImageDto>();
            var root = HostingEnvironment.MapPath("~/images/");
            if (!Directory.Exists(root)) Directory.CreateDirectory(root);


            foreach (MultipartFileData file in files)
            {
                var image = new ImageDto();

                //Add the original extention to the file
                string imageName = Guid.NewGuid() + Path.GetExtension(file.Headers.ContentDisposition.FileName.Replace("\"", ""));
                string newFileName = String.Format("{0}{1}", root, imageName);
                File.Move(file.LocalFileName, newFileName);
                // Load image.
                using (Image imageThumbnail = Image.FromFile(newFileName))
                {
                    // Compute thumbnail size.
                    Size thumbnailSize = GetThumbnailSize(imageThumbnail);
                    // Get thumbnail.
                    using (Image thumbnail = imageThumbnail.GetThumbnailImage(thumbnailSize.Width,
                        thumbnailSize.Height, null, IntPtr.Zero))
                    {

                        string imageThumbanilName = "thumbnail_" + imageName;

                        thumbnail.Save(String.Format("{0}{1}", root, imageThumbanilName));
                        image.Name = imageName;
                        image.Thumbnail = imageThumbanilName;

                        images.Add(image);
                    }
                }
            }
            return images;
        }

        private Size GetThumbnailSize(Image original)
        {
            // Maximum size of any dimension.
            const int maxPixels = 80;

            // Width and height.
            int originalWidth = original.Width;
            int originalHeight = original.Height;

            // Compute best factor to scale entire image based on larger dimension.
            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double)maxPixels / originalWidth;
            }
            else
            {
                factor = (double)maxPixels / originalHeight;
            }

            // Return thumbnail size.
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }
    }
}