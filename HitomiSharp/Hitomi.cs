using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HitomiSharp
{
    public static class Hitomi
    {
        public static bool IsLoaded = false;
        private static GalleryInfo[] _galleries;
        private readonly static Regex jsonCountRegex = new Regex(@"number_of_gallery_jsons\s?=\s?([0-9]+)");
        
        public static GalleryInfo? GetGalleryInfo(int id)
        {
            if (!IsLoaded)
                throw new Exception("아직 갤러리 목록이 안불러와졌음");
            var matches = _galleries.Where(g => g.ID == id);
            return matches.Count() == 0 ? (GalleryInfo?)null : matches.First();
        }

        public static IEnumerable<GalleryInfo> FilterGalleries(SearchOption option)
        {

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
                    var galleries = serializer.Deserialize<GalleryInfo[]>(jr);
                    foreach (var g in galleries)
                        result.Add(g);
                });

            }
        }

        public static async Task LoadAllGalleriesAsync()
        {
            var count = await GetJsonCouunt();
            ConcurrentBag<GalleryInfo> result = new ConcurrentBag<GalleryInfo>();

            var tasks = new List<Task>();
            for (int i = 0; i < count; i++)
            {
                tasks.Add(DownloadChunk(i, result));
            }
            await Task.WhenAll(tasks.ToArray())
                .ContinueWith((t) => {
                    IsLoaded = true;
                    _galleries = result.ToArray();
                });
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
