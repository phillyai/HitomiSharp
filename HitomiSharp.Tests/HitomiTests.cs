using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HitomiSharp.Tests
{
    [TestClass]
    public class HitomiTests
    {
        [TestMethod]
        public async void GetGalleryInfoAsync()
        {
            var info = await Hitomi.GetGalleryInfoAsync(405092);

            Assert.AreEqual("Sora No Omocha", info.Title);
            Assert.AreEqual(new[] { "hiten onee-ryuu" }, info.Artists);
            Assert.AreEqual(new[] { "shadow sorceress communication protocol" }, info.Groups);
            Assert.AreEqual("doujinshi", info.Type);
            Assert.AreEqual(Language.Korean, info.Language);
            Assert.AreEqual(new[] { "yosuga no sora" }, info.Series);
            Assert.AreEqual(new[] { "sora kasugano" }, info.Characters);
            Assert.AreEqual(new[] { "female:femdom", "female:loli", "female:sister", "female:stocking", "incest" }, info.Tags);
        }
    }
}
