using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using webUI.Common;

namespace webUI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SearchTools st = new SearchTools();

            string url = st.CreateSearchURL("the fox ran into the woods");
        }
    }
}
