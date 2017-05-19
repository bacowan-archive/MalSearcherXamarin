using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NUnit.Framework;
using MalSearcher.Model.MalProxy;

namespace UnitTestApp2.MalProxy
{
    [TestFixture]
    class TestTest
    {
        private WebGetter mWebGetter;
        [SetUp]
        public void Setup()
        {
            mWebGetter = new WebGetter();
        }

        [Test]
        public void TestReadValidCharacterList()
        {
            string result = mWebGetter.Get("https://myanimelist.net/malappinfo.php?u=DoomInAJar2&status=all&type=anime");
            int i = 1;
        }
    }
}