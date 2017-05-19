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
using Database.Objects;
using Database;
using MalSearcher.Model.MalProxy.Parsers;

namespace UnitTestApp2.MalProxy
{
    [TestFixture]
    class TestSearchResultParser
    {
        private const string VALID_SEARCH_RESULTS_FILE_NAME = "valid_search_results.xml";

        private SearchResultParser mParser;
        private string mAnimeSearchResults;


        [SetUp]
        public void Setup()
        {
            mParser = new SearchResultParser();
            mAnimeSearchResults = TestUtils.ReadFile(Application.Context, VALID_SEARCH_RESULTS_FILE_NAME);
        }

        [Test]
        public void TestReadValidSearchResult()
        {
            ICollection<Anime> anime = mParser.Parse(mAnimeSearchResults);
            Assert.AreEqual(8, anime.Count);
        }

        [Test]
        public void TestSingleValidSearchResult()
        {
            ICollection<Anime> animeList = mParser.Parse(mAnimeSearchResults);

            Assert.True(animeList.Select(a => a.AnimeID).Contains(71));
            Anime anime = animeList.Where(a => a.AnimeID == 71).Single();

            Assert.AreEqual("Full Metal Panic!", anime.Title);
            Assert.AreEqual(new DateTime(2002, 1, 8), anime.Start);
            Assert.AreEqual(new DateTime(2002, 6, 18), anime.End);
            Assert.AreEqual(SeriesStatus.FinishedAiring, anime.Status);
            Assert.AreEqual(SeriesType.TV, anime.Type);
            Assert.AreEqual(24, anime.Episodes);
            Assert.AreEqual("FMP; Fullmetal Panic!", anime.Synonyms);
            Assert.AreEqual("https://myanimelist.cdn-dena.com/images/anime/2/75259.jpg", anime.ImageURL);
            Assert.True(anime.Synopsis.StartsWith("Equipped with cutting-edge"));
        }
    }
}