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
using Database;
using Database.Objects;
using MalSearcher.Model.MalProxy.Parsers;
using Database.Objects.Intersections;

namespace UnitTestApp2.MalProxy
{
    [TestFixture]
    class TestCharacterListParser
    {
        private const string VALID_LIST_FILE_NAME = "valid_anime2.html";

        private CharacterListParser mParser;
        private string mAnimeHtml;

        [SetUp]
        public void Setup()
        {
            mParser = new CharacterListParser();
            mAnimeHtml = TestUtils.ReadFile(Application.Context, VALID_LIST_FILE_NAME);
        }

        [Test]
        public void TestReadValidCharacterList()
        {
            ICollection<Character> result = mParser.Parse(mAnimeHtml);
            Assert.AreEqual(18, result.Count);
        }

        [Test]
        public void TestReadValidCharacterListEntry()
        {
            int characterId = 227;
            ICollection<Character> result = mParser.Parse(mAnimeHtml);
            Assert.True(result.Select(r => r.CharacterID).Contains(characterId));

            Character character = result.Where(r => r.CharacterID == characterId).Distinct().SingleOrDefault();
            Assert.AreEqual(characterId, character.CharacterID);
            Assert.AreEqual("Chii", character.Name.English);
        }

        [Test]
        public void TestReadValidCharacterActors()
        {
            int characterId = 227;
            int actorId = 85;
            ICollection<Character> result = mParser.Parse(mAnimeHtml);

            IEnumerable<CharacterActor> characterActors = result.Where(r => r.CharacterID == characterId).SingleOrDefault().Actors;
            Assert.AreEqual(5, characterActors.Count());
            Assert.True(characterActors.Select(ca => ca.Actor.VoiceActorID).Contains(actorId));

            CharacterActor characterActor = characterActors.Where(ca => ca.Actor.VoiceActorID == actorId).SingleOrDefault();
            Assert.AreEqual("Michelle Ruff", characterActor.Actor.Name.English);
            Assert.AreEqual("English", characterActor.Language.ToString());
        }

    }
}