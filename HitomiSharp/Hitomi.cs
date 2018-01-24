using System;
using System.Threading.Tasks;

namespace HitomiSharp
{
    public class Hitomi
    {
        public static async Task<GalleryInfo> GetGalleryInfoAsync(int id)
            => await GetGalleryInfoAsync(id.ToString());
        public static async Task<GalleryInfo> GetGalleryInfoAsync(string id)
        {
            throw new NotImplementedException();
        }

        public static async Task<string[]> GetImageUrls(int id)
            => await GetImageUrls(id.ToString());
        public static async Task<string[]> GetImageUrls(string id)
        {
            var url = $"https://hitomi.la/galleries/{id}.js";
            var content = url.Substring("var galleryinfo = ".Length);

            throw new NotImplementedException();
        }
    }
}
