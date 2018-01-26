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

        private static async Task DownloadChunk(int i, ConcurrentBag<GalleryInfo> result)
        {
            var url = $"https://ltn.hitomi.la/galleries{i}.json";
            var req = CreateRequest(url);
            using (var w = await req.GetResponseAsync())
            using (var s = w.GetResponseStream())
            using (var sr = new StreamReader(s))
            using (var jr = new JsonTextReader(sr))
            {
                await Task.Run(() =>
                {
                    var serializer = new JsonSerializer();
                    System.Diagnostics.Debug.WriteLine($"{i} download start at {DateTime.Now.ToString("tt h:mm:ss.ffff")}", "LOG");
                    var galleries = serializer.Deserialize<GalleryInfo[]>(jr);
                    System.Diagnostics.Debug.WriteLine($"{i} download end, start appending at {DateTime.Now.ToString("tt h:mm:ss.ffff")}", "LOG");
                    foreach (var g in galleries)
                        result.Add(g);
                    System.Diagnostics.Debug.WriteLine($"{i} append end at {DateTime.Now.ToString("tt h:mm:ss.ffff")}", "LOG");
                });

            }
        }

        public static async Task<GalleryInfo[]> GetAllGalleriesAsync()
        {
            System.Diagnostics.Debug.WriteLine($"Start at {DateTime.Now.ToString("tt h:mm:ss.ffff")}", "Gallaries");
            var count = await GetJsonCouunt();
            System.Diagnostics.Debug.WriteLine($"Got count at {DateTime.Now.ToString("tt h:mm:ss.ffff")}", "Gallaries");
            ConcurrentBag<GalleryInfo> result = new ConcurrentBag<GalleryInfo>();
            System.Diagnostics.Debug.WriteLine($"Created concurrentbag at {DateTime.Now.ToString("tt h:mm:ss.ffff")}", "Gallaries");

            var tasks = new List<Task>();
            for (int i = 0; i < count; i++)
            {
                tasks.Add(DownloadChunk(i, result));
            }
            System.Diagnostics.Debug.WriteLine($"Start tasks at {DateTime.Now.ToString("tt h:mm:ss.ffff")}", "Gallaries");
            await Task.WhenAll(tasks.ToArray());
            System.Diagnostics.Debug.WriteLine($"Ran all tasks at {DateTime.Now.ToString("tt h:mm:ss.ffff")}", "Gallaries");
            var temp = result.ToArray();
            System.Diagnostics.Debug.WriteLine($"toarray ended at {DateTime.Now.ToString("tt h:mm:ss.ffff")}", "Gallaries");
            return temp;
        }

        private static HttpWebRequest CreateRequest(string url)
        {
            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            req.Method = "GET";
            req.Accept = "*/*";
            req.Referer = "https://hitomi.la";
            req.UserAgent = "Mozilla/5.0";
            req.KeepAlive = true;
            req.ServicePoint.ConnectionLimit = 100;
            req.ServicePoint.Expect100Continue = false;
            req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
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
