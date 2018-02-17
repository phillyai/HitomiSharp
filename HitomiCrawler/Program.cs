using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using HitomiSharp;

namespace HitomiCrawler
{
    class Program
    {
        async static Task Main(string[] args)
        {
            await Task.WhenAll(Hitomi.LoadAllGalleriesAsync());
            var galleries = Hitomi.FilterGalleries(new SearchOption()
            {
                Language = "korean"
            });


            foreach (var g in galleries)
            {
                await DownloadGallery(g);
            }
        }

        async static Task DownloadGallery(GalleryInfo g)
        {
            var client = new WebClient();
            System.Console.WriteLine($"Downloading {g.ID} - {g.Title}..");
            var path = System.IO.Path.Combine(
                System.IO.Directory.GetCurrentDirectory(),
                "Downloads",
                System.IO.Path.GetInvalidFileNameChars().Aggregate($"{g.ID} - {g.Title}", (cur, c) => cur.Replace(c.ToString(), string.Empty)));
            foreach (var c in System.IO.Path.GetInvalidPathChars())
                path = path.Replace(c.ToString(), "");
            System.IO.Directory.CreateDirectory(path);
            var urls = await Hitomi.GetImageUrls(g.ID);
            foreach (var url in urls)
            {
                var file = System.IO.Path.Combine(path, url.Split('/').Last());
                await client.DownloadFileTaskAsync(url, file);
            }
        }
    }
}
