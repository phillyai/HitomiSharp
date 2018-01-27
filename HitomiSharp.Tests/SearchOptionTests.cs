using System.linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HitomiSharp.Tests
{
    [TestClass]
    public class SearchOptionTests
    {
        [TestMethod, TestCategory("Search")]
        public void SearchWithoutFilterTest()
        {
            string[] hohofuckingcsharp = { "this is perfectly", "working" };
            var guroFilter = new SearchOption()
            {
                TagsWithout = { "female:guro", "female:netorare" }
            };
            System.Runtime.CompilerServices.RuntimeHelpers.InitializeArray()
        }

        [TestMethod, TestCategory("Search")]
        public void SearchFilterTest(string[] args)
        {

        }
    }
}
