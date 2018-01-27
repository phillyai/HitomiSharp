using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HitomiSharp.Tests
{
    [TestClass]
    public class SearchOptionTests
    {
        [TestMethod, TestCategory("Search")]
        public void SearchWithoutFilterTest()
        {
            var guroFilter = new SearchOption()
            {
                TagsWithout = new []{ "female:guro", "female:netorare" }
            };

            var holy = new GalleryInfo(id: 1,
                title: "it's trap shit",
                artists: new string[] { "ero manga sensei" },
                groups: new string[] { },
                type: "doujinshi",
                language: "korean",
                series: new string[] { },
                characters: new string[] { },
                tags: new string[] { "male:males only", "male:guro" });
            
            Assert.IsTrue(guroFilter.IsProfit(holy));

            var shit = new GalleryInfo(id: 1,
                title: "help me",
                artists: new string[] { "santa claus" },
                groups: new string[] { },
                type: "doujinshi",
                language: "korean",
                series: new string[] { },
                characters: new string[] { },
                tags: new string[] { "female:schoolgirl uniform", "female:guro" });

            Assert.IsFalse(guroFilter.IsProfit(shit));

            var empty = new GalleryInfo(id: 1,
                title: "help me",
                artists: new string[] { "santa claus" },
                groups: new string[] { },
                type: "doujinshi",
                language: "korean",
                series: new string[] { },
                characters: new string[] { },
                tags: new string[] { });

            Assert.IsTrue(guroFilter.IsProfit(empty));

            var artistFilter = new SearchOption()
            {
                ArtistsWithout = new[] { "tagame gengoroh", "gengoroh", "fucking tagame gengoroh" },
                TagsWithout = new [] { "male:bdsm" }
            };

            var hardcoregaysex = new GalleryInfo(id: 1,
                title: "let's look your back",
                artists: new string[] { "gengoroh" },
                groups: new string[] { },
                type: "doujinshi",
                language: "korean",
                series: new string[] { },
                characters: new string[] { },
                tags: new string[] { "male:males only", "male:bdsm" });

            Assert.IsFalse(artistFilter.IsProfit(hardcoregaysex));

            var nothardcoregaysex = new GalleryInfo(id: 1,
                 title: "shota rules",
                 artists: new string[] { "ayato ayari" },
                 groups: new string[] { },
                 type: "doujinshi",
                 language: "korean",
                 series: new string[] { },
                 characters: new string[] { },
                 tags: new string[] { "male:males only" });

            Assert.IsTrue(artistFilter.IsProfit(nothardcoregaysex));

            var anywaygaysex = new GalleryInfo(id: 1,
                 title: "이짓거리하고있으니까죽고싶다살려줘",
                 artists: new string[] { },
                 groups: new string[] { },
                 type: "doujinshi",
                 language: "korean",
                 series: new string[] { },
                 characters: new string[] { },
                 tags: new string[] { "male:netorare", "male:bdsm" });

            Assert.IsFalse(artistFilter.IsProfit(anywaygaysex));
        }

        [TestMethod, TestCategory("Search")]
        public void SearchFilterTest()
        {
            var criminal = new SearchOption()
            {
                Language = "korean",
                Tags = new[] { "female:loli", "male:shota" }
            };

            var dangerous = new GalleryInfo(id: 1,
                title: "go to 911 please",
                artists: new string[] { },
                groups: new string[] { },
                type: "doujinshi",
                language: "korean",
                series: new string[] { },
                characters: new string[] { },
                tags: new string[] { "female:loli", "female:schoolgirl uniform", "male:shota" });

            Assert.IsTrue(criminal.IsProfit(dangerous));

            var notenough = new GalleryInfo(id: 1,
                title: "go to 911 please",
                artists: new string[] { },
                groups: new string[] { },
                type: "doujinshi",
                language: "korean",
                series: new string[] { },
                characters: new string[] { },
                tags: new string[] { "female:loli", "female:schoolgirl uniform" });

            Assert.IsFalse(criminal.IsProfit(notenough));
        }

        [TestMethod, TestCategory("Search")]
        public void SearchByNameTest()
        {
            var ponytail = new SearchOption()
            {
                Title = "ponytail",
                Language = "korean"
            };

            var empty = new GalleryInfo(id: 1,
                title: "      ",
                artists: new string[] { },
                groups: new string[] { },
                type: "doujinshi",
                language: "korean",
                series: new string[] { },
                characters: new string[] { },
                tags: new string[] { });

            Assert.IsFalse(ponytail.IsProfit(empty));

            var perfect = new GalleryInfo(id: 1,
                title: "kimi dake no ponytail",
                artists: new string[] { },
                groups: new string[] { },
                type: "doujinshi",
                language: "korean",
                series: new string[] { },
                characters: new string[] { },
                tags: new string[] { });

            Assert.IsTrue(ponytail.IsProfit(perfect));

            var upper = new GalleryInfo(id: 1,
                title: "Kimi Dake No PonyTail",
                artists: new string[] { },
                groups: new string[] { },
                type: "doujinshi",
                language: "korean",
                series: new string[] { },
                characters: new string[] { },
                tags: new string[] { });

            Assert.IsTrue(ponytail.IsProfit(upper));
        }
    }
}
