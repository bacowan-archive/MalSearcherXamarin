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
using MalSearcher.Model.MalProxy.Parsers;

namespace UnitTestApp2.MalProxy
{
    [TestFixture]
    class TestCharacterListLinkParser
    {
        private const string VALID_LIST_FILE_NAME = "valid_anime_main_page.html";

        private CharacterListLinkParser mParser;
        private string mAnimeHtml;

        [SetUp]
        public void Setup()
        {
            mParser = new CharacterListLinkParser();
            mParser.ID = 59;
            mAnimeHtml = TestUtils.ReadFile(Application.Context, VALID_LIST_FILE_NAME);
        }

        [Test]
        public void TestReadValidLink()
        {
            string result = mParser.Parse(mAnimeHtml);
            Assert.AreEqual("https://myanimelist.net/anime/59/Chobits/characters", result);
        }
    }
}