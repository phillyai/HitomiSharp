using System.Linq;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HitomiSharp.Tests
{
    [TestClass]
    public class HitomiTests
    {
        [TestMethod]
        public void GetGalleryInfoAsyncTest()
        {
            GalleryInfo info = Hitomi.GetGalleryInfoAsync(405092).Result;

            Assert.AreEqual("Sora No Omocha", info.Title);
            Assert.AreEqual(new[] { "hiten onee-ryuu" }, info.Artists);
            Assert.AreEqual(new[] { "shadow sorceress communication protocol" }, info.Groups);
            Assert.AreEqual("doujinshi", info.Type);
            Assert.AreEqual("korean", info.Language);
            Assert.AreEqual(new[] { "yosuga no sora" }, info.Series);
            Assert.AreEqual(new[] { "sora kasugano" }, info.Characters);
            Assert.AreEqual(new[] { "female:femdom", "female:loli", "female:sister", "female:stocking", "incest" }, info.Tags);

            info = Hitomi.GetGalleryInfoAsync(998175).Result;

            Assert.AreEqual("Kimi Dake no Ponytail", info.Title);
            Assert.AreEqual(new[] { "konayama kata" }, info.Artists);
            Assert.AreEqual(new[] { "canaria" }, info.Groups);
            Assert.AreEqual("doujinshi", info.Type);
            Assert.AreEqual("korean", info.Language);
            Assert.AreEqual(new string[] { }, info.Series);
            Assert.AreEqual(new string[] { }, info.Characters);
            Assert.AreEqual(new[] { "male:anal", "male:crossdressing", "male:males only", "male:schoolgirl uniform", "male:shota", "male:tomgirl", "male:yaoi" }, info.Tags);
        }
        
        [TestMethod]
        public void DeserializeTest()
        {
            GalleryInfo info;
            string json = "{\"n\":\"INKOU THE PLAY NS\",\"l\":\"korean\",\"a\":[\"chicago\"],\"id\":1174847,\"type\":\"doujinshi\",\"t\":[\"female:sole female\"],\"c\":[\"aoi zaizen\"],\"g\":[\"4or5 works\"],\"p\":[\"yu-gi-oh vrains\"]}";
            using (var m = new System.IO.MemoryStream())
            {
                var w = new System.IO.StreamWriter(m);
                w.Write(json);
                w.Flush();
                m.Position = 0;
                using (var r = new System.IO.StreamReader(m))
                using (var jr = new JsonTextReader(r))
                {
                    var serializer = new JsonSerializer();
                    info = serializer.Deserialize<GalleryInfo>(jr);
                }
            }

            Assert.AreEqual(1174847, info.ID);
            Assert.AreEqual("INKOU THE PLAY NS", info.Title);
            CollectionAssert.AreEqual(new string[] { "chicago" }, info.Artists);
            CollectionAssert.AreEqual(new string[] { "4or5 works" }, info.Groups);
            Assert.AreEqual("doujinshi", info.Type);
            Assert.AreEqual("korean", info.Language);
            CollectionAssert.AreEqual(new string[] { "yu-gi-oh vrains" }, info.Series);
            CollectionAssert.AreEqual(new string[] { "aoi zaizen" }, info.Characters);
            CollectionAssert.AreEqual(new string[] { "female:sole female" }, info.Tags);
        }

        [TestMethod, TestCategory("speed")]
        public void GetAllGalleriesAsyncTest()
        {
            GalleryInfo[] galleries = Hitomi.GetAllGalleriesAsync().Result;
            var matches = galleries.Where(g => g.ID == 405092);
            var match = matches.First();
            Assert.IsTrue(galleries.Length > 0);
        }
        /*
        [TestMethod, TestCategory("speed")]
        public void GetCountSpeedTest()
        {
            var count = Hitomi.GetJsonCouunt().Result;
        }

        [TestMethod, TestCategory("speed")]
        public void Download1ChunkSpeedTest()
        {
            var galleries = new System.Collections.Concurrent.ConcurrentBag<GalleryInfo>();
            System.Threading.Tasks.Task.WaitAll(Hitomi.DownloadChunk(0, galleries));
        }

        [TestMethod, TestCategory("speed")]
        public void Download2ChunksSpeedTest()
        {
            var galleries = new System.Collections.Concurrent.ConcurrentBag<GalleryInfo>();
            var tasks = new System.Collections.Generic.List<System.Threading.Tasks.Task>();
            for (int i = 0; i < 2; i++)
                tasks.Add(Hitomi.DownloadChunk(i, galleries));
            System.Threading.Tasks.Task.WaitAll(tasks.ToArray());
        }

        [TestMethod, TestCategory("speed")]
        public void Download3ChunksSpeedTest()
        {
            var galleries = new System.Collections.Concurrent.ConcurrentBag<GalleryInfo>();
            var tasks = new System.Collections.Generic.List<System.Threading.Tasks.Task>();
            for (int i = 0; i < 3; i++)
                tasks.Add(Hitomi.DownloadChunk(i, galleries));
            System.Threading.Tasks.Task.WaitAll(tasks.ToArray());
        }

        [TestMethod, TestCategory("speed")]
        public void Download5ChunksSpeedTest()
        {
            var galleries = new System.Collections.Concurrent.ConcurrentBag<GalleryInfo>();
            var tasks = new System.Collections.Generic.List<System.Threading.Tasks.Task>();
            for (int i = 0; i < 5; i++)
                tasks.Add(Hitomi.DownloadChunk(i, galleries));
            System.Threading.Tasks.Task.WaitAll(tasks.ToArray());
        }

        [TestMethod, TestCategory("speed")]
        public void Download10ChunksSpeedTest()
        {
            var galleries = new System.Collections.Concurrent.ConcurrentBag<GalleryInfo>();
            var tasks = new System.Collections.Generic.List<System.Threading.Tasks.Task>();
            for (int i = 0; i < 10; i++)
                tasks.Add(Hitomi.DownloadChunk(i, galleries));
            System.Threading.Tasks.Task.WaitAll(tasks.ToArray());
        }

        [TestMethod, TestCategory("speed")]
        public void Download15ChunksSpeedTest()
        {
            var galleries = new System.Collections.Concurrent.ConcurrentBag<GalleryInfo>();
            var tasks = new System.Collections.Generic.List<System.Threading.Tasks.Task>();
            for (int i = 0; i < 15; i++)
                tasks.Add(Hitomi.DownloadChunk(i, galleries));
            System.Threading.Tasks.Task.WaitAll(tasks.ToArray());
        }

        [TestMethod, TestCategory("speed")]
        public void Download20ChunksSpeedTest()
        {
            var galleries = new System.Collections.Concurrent.ConcurrentBag<GalleryInfo>();
            var tasks = new System.Collections.Generic.List<System.Threading.Tasks.Task>();
            for (int i = 0; i < 20; i++)
                tasks.Add(Hitomi.DownloadChunk(i, galleries));
            System.Threading.Tasks.Task.WaitAll(tasks.ToArray());
        }
        */
    }
}
