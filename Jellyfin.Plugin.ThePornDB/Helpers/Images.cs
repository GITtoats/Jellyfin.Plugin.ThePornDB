using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaBrowser.Model.Entities;
using Newtonsoft.Json.Linq;
using ThePornDB.Configuration;

namespace ThePornDB.Helpers
{
    public class ImageList
    {

        public static List<(ImageType Type, string Url)>GetImageList(JObject data)
        {

            string img_url = (string)data["image"],
                full_url = (string)data["background"]["full"],
                large_url = (string)data["background"]["large"],
                poster_url = (string)data["poster"],
                poster_large_url = (string)data["posters"]["large"];



            var images = new List<(ImageType Type, string Url)> {};

            var poster_all = new List<(ImageType Type, string Url)>
            {
                (ImageType.Primary, img_url),
                (ImageType.Primary, full_url),
                (ImageType.Primary, large_url),
                (ImageType.Primary, poster_url),
                (ImageType.Primary, poster_large_url),
            };

            var backdrop_all = new List<(ImageType Type, string Url)>
            {
                (ImageType.Backdrop, img_url),
                (ImageType.Backdrop, full_url),
                (ImageType.Backdrop, large_url),
            };

            switch (Plugin.Instance.Configuration.ThumbImage)
            {
                case ThumbImageStyle.All:
                    images.Add(new() { Type = ImageType.Thumb, Url = img_url });
                    images.Add(new() { Type = ImageType.Thumb, Url = full_url });
                    break;
                case ThumbImageStyle.Full:
                    images.Add(new() { Type = ImageType.Thumb, Url = full_url });
                    break;              
                case ThumbImageStyle.Image:
                    images.Add(new() { Type = ImageType.Thumb, Url = img_url });
                    break;

            }
            switch (Plugin.Instance.Configuration.PosterImage)
            {
                case PosterImageStyle.All:
                    images = images.Concat(poster_all).ToList();
                    break;
                case PosterImageStyle.Full:
                    images.Add(new() { Type = ImageType.Primary, Url = poster_url });
                    images.Add(new() { Type = ImageType.Primary, Url = full_url });
                    break;
                case PosterImageStyle.Large:
                    images.Add(new() { Type = ImageType.Primary, Url = poster_large_url });
                    images.Add(new() { Type = ImageType.Primary, Url = large_url });
                    break;
                case PosterImageStyle.Image:
                    images.Add(new() { Type = ImageType.Primary, Url = img_url });
                    images.Add(new() { Type = ImageType.Primary, Url = poster_url });
                    break;
                case PosterImageStyle.Posters:
                    images.Add(new() { Type = ImageType.Primary, Url = poster_url });
                    images.Add(new() { Type = ImageType.Primary, Url = poster_large_url });
                    break;                  
            }
            switch (Plugin.Instance.Configuration.BackdropImage)
            {
                case BackdropImageStyle.All:
                    images = images.Concat(backdrop_all).ToList();
                    break;
                case BackdropImageStyle.Full:
                    images.Add(new() { Type = ImageType.Backdrop, Url = full_url });
                    break;
                case BackdropImageStyle.Large:
                    images.Add(new() { Type = ImageType.Backdrop, Url = large_url });
                    break;
                case BackdropImageStyle.Image:
                    images.Add(new() { Type = ImageType.Backdrop, Url = img_url });
                    break;
                case BackdropImageStyle.Full_Large:
                    images.Add(new() { Type = ImageType.Backdrop, Url = full_url });
                    images.Add(new() { Type = ImageType.Backdrop, Url = large_url });
                    break;

            }
            return images;
        }
    }
}
