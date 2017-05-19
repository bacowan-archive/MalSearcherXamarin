using System;
using Android.App;
using MalSearcher.Model.MalProxy;
using NUnit.Framework;
using Database;
using System.Linq;
using Database.Objects;
using MalSearcher.Model.MalProxy.Parsers;
using MalSearcher.Model.MalProxy.Consolidators;

namespace UnitTestApp2.MalProxy
{
    [TestFixture]
    public class TestUserAnimeListParser
    {
        private const string VALID_LIST_FILE_NAME = "samplelist.xml";

        private UserAnimeListParser mParser;
        private string mAnimeXml;

        [SetUp]
        public void Setup()
        {
            mParser = new UserAnimeListParser();
            mAnimeXml = TestUtils.ReadFile(Application.Context, VALID_LIST_FILE_NAME);
        }

        [Test]
        public void TestReadValidUser()
        {
            User result = mParser.Parse(mAnimeXml);
            
            Assert.AreEqual(4440225, result.UserId);
            Assert.AreEqual("DoomInAJar2", result.Username);
            Assert.AreEqual(1, result.Watching);
            Assert.AreEqual(2, result.Completed);
            Assert.AreEqual(3, result.OnHold);
            Assert.AreEqual(4, result.Dropped);
            Assert.AreEqual(0.45, result.DaysSpentWatching);
            Assert.AreEqual(5, result.AnimeList.Count);
        }

        [Test]
        public void TestReadValidMyAnimeEntries()
        {
            User result = mParser.Parse(mAnimeXml);
            MyAnimeEntry entry = result.AnimeList.Where(l => l.Anime.AnimeID == 1177).SingleOrDefault();

            Assert.AreEqual(0, entry.MyIdValue);
            Assert.AreEqual(2, entry.WatchedEpisodes);
            Assert.AreEqual(new DateTime(1987, 12, 01), entry.StartDate);
            Assert.IsNull(entry.EndDate);
            Assert.AreEqual(0, entry.Score);
            Assert.AreEqual(WatchStatus.CurrentlyWatching, entry.Status);
            Assert.AreEqual(RewatchValue.None, entry.RewatchValue);
            Assert.AreEqual(0, entry.RewatchedEpisodes);
            Assert.AreEqual(1422081919, entry.LastUpdated);
        }

        [Test]
        public void TestReadValidAnime()
        {
            User result = mParser.Parse(mAnimeXml);
            Anime anime = result.AnimeList.Where(l => l.Anime.AnimeID == 1177).SingleOrDefault().Anime;


            Assert.AreEqual(1177, anime.AnimeID);
            Assert.AreEqual("Alien 9", anime.Title);
            Assert.AreEqual("Alien Nine", anime.Synonyms);
            Assert.AreEqual(SeriesType.OVA, anime.Type);
            Assert.AreEqual(4, anime.Episodes);
            Assert.AreEqual(SeriesStatus.FinishedAiring, anime.Status);
            Assert.AreEqual(new DateTime(2001, 6, 25), anime.Start);
            Assert.AreEqual(new DateTime(2002, 2, 25), anime.End);
            Assert.AreEqual("https://myanimelist.cdn-dena.com/images/anime/7/2607.jpg", anime.ImageURL);
        }
    }
}