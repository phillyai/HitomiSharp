using System;
using System.Threading.Tasks;
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
            Assert.AreEqual(Language.korean, info.Language);
            Assert.AreEqual(new[] { "yosuga no sora" }, info.Series);
            Assert.AreEqual(new[] { "sora kasugano" }, info.Characters);
            Assert.AreEqual(new[] { "female:femdom", "female:loli", "female:sister", "female:stocking", "incest" }, info.Tags);
            
            info = Hitomi.GetGalleryInfoAsync(998175).Result;

            Assert.AreEqual("Kimi Dake no Ponytail", info.Title);
            Assert.AreEqual(new[] { "konayama kata" }, info.Artists);
            Assert.AreEqual(new[] { "canaria" }, info.Groups);
            Assert.AreEqual("doujinshi", info.Type);
            Assert.AreEqual(Language.korean, info.Language);
            Assert.AreEqual(new string[] { }, info.Series);
            Assert.AreEqual(new string[] { }, info.Characters);
            Assert.AreEqual(new[] { "male:anal", "male:crossdressing", "male:males only", "male:schoolgirl uniform", "male:shota", "male:tomgirl", "male:yaoi" }, info.Tags);
        }

        [TestMethod]
        public void GetAllGalleriesAsyncTest()
        {
            GalleryInfo[] galleries = Hitomi.GetAllGalleriesAsync().Result;
            Assert.IsTrue(galleries.Length > 0);
        }
    }
}
