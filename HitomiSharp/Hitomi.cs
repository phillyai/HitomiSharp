using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HitomiSharp
{
    public class Hitomi
    {
        private readonly static Regex jsonCountRegex = new Regex(@"number_of_gallery_jsons\s?=\s?([0-9]+)");
        
        public static async Task<GalleryInfo> GetGalleryInfoAsync(int id)
        {
            throw new NotImplementedException();
        }
        
        public static async Task<string[]> GetImageUrls(int id)
        {
            var url = $"https://hitomi.la/galleries/{id}.js";
            var content = url.Substring("var galleryinfo = ".Length);

            throw new NotImplementedException();
        }

        private static async Task<int> GetJsonCouunt()
        {
            var url = "https://hitomi.la/searchlib.js";
            var content = await GetResponseAsync(url);
            return Convert.ToInt32(jsonCountRegex.Match(content).Groups[1].Value);
        }

        public static async Task<GalleryInfo[]> GetAllGalleriesAsync()
        {
            var count = 1;//await GetJsonCouunt();
            ConcurrentBag<GalleryInfo> result = new ConcurrentBag<GalleryInfo>();

            async Task DownloadChunk(int i)
            {
                var url = $"https://hitomi.la/galleries{i}.json";
                var req = CreateRequest(url);
                using (var w = await req.GetResponseAsync())
                using (var s = w.GetResponseStream())
                using (var sr = new StreamReader(s))
                using (var jr = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    var galleries = serializer.Deserialize<GalleryInfo[]>(jr);
                    foreach (var g in galleries)
                        result.Add(g);
                }
            }

            var tasks = new List<Task>();
            for (int i = 0; i < count; i++)
            {
                tasks.Add(DownloadChunk(i));
            }
            await Task.WhenAll(tasks);
            return result.ToArray();
        }

        private static HttpWebRequest CreateRequest(string url)
        {
            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            req.Method = "GET";
            req.Accept = "*/*";
            req.Referer = "https://hitomi.la";
            req.UserAgent = "Mozilla/5.0";
            req.KeepAlive = true;
            return req;
        }

        private static async Task<string> GetResponseAsync(string url)
        {
            var req = CreateRequest(url);

            using (var w = await req.GetResponseAsync())
            using (var s = w.GetResponseStream())
            using (var sr = new StreamReader(s))
            {
                return await sr.ReadToEndAsync();
            }
        }
    }
}
