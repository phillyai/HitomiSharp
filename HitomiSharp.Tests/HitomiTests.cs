using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HitomiSharp.Tests
{
    [TestClass]
    public class HitomiTests
    {
        public HitomiTests()
        {
            Hitomi.LoadAllGalleriesAsync().Wait();
        }

        [TestMethod, TestCategory("Hitomi")]
        public void GetGalleryInfoAsyncTest()
        {
            var val = Hitomi.GetGalleryInfo(405092);
            Assert.IsNotNull(val);
            var info = val.Value;

            Assert.AreEqual("Sora no Omocha", info.Title);
            CollectionAssert.AreEqual(new[] { "hiten onee-ryuu" }, info.Artists);
            CollectionAssert.AreEqual(new[] { "shadow sorceress communication protocol" }, info.Groups);
            Assert.AreEqual("doujinshi", info.Type);
            Assert.AreEqual("korean", info.Language);
            CollectionAssert.AreEqual(new[] { "yosuga no sora" }, info.Series);
            CollectionAssert.AreEqual(new[] { "sora kasugano" }, info.Characters);
            CollectionAssert.AreEqual(new[] { "female:femdom", "female:loli", "female:sister", "female:stockings", "incest" }, info.Tags);

            val = Hitomi.GetGalleryInfo(998175);
            Assert.IsNotNull(val);
            info = val.Value;

            Assert.AreEqual("Kimi Dake no Ponytail", info.Title);
            CollectionAssert.AreEqual(new[] { "konayama kata" }, info.Artists);
            CollectionAssert.AreEqual(new[] { "canaria" }, info.Groups);
            Assert.AreEqual("doujinshi", info.Type);
            Assert.AreEqual("korean", info.Language);
            Assert.IsNull(info.Series);
            Assert.IsNull(info.Characters);
            CollectionAssert.AreEqual(new[] { "male:anal", "male:crossdressing", "male:males only", "male:schoolgirl uniform", "male:shota", "male:tomgirl", "male:yaoi" }, info.Tags);
        }

        [TestMethod, TestCategory("Hitomi")]
        public void GetImageUrlsTest()
        {
            var expected = (from i in Enumerable.Range(1, 28)
                            select $"https://a.hitomi.la/galleries/405092/{i:000}.jpg")
                            .Concat(new string[] {
                                "https://a.hitomi.la/galleries/405092/a001.jpg",
                                "https://a.hitomi.la/galleries/405092/a002.jpg",
                            }).ToArray();
            var actual = Hitomi.GetImageUrls(405092).Result;

            CollectionAssert.AreEqual(expected, actual);
        }
        
        [TestMethod, TestCategory("Hitomi")]
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

        [TestMethod, TestCategory("Hitomi")]
        public void GetAllGalleriesAsyncTest()
        {
            Assert.IsTrue(Hitomi.IsLoaded);
        }
    }
}
