using Database;
using Database.Context;
using Database.Objects;
using MalSearcher.Model.MalProxy.Consolidators;
using MalSearcher.Model.MalProxy.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using Database.Objects.Intersections;
using System.Collections.ObjectModel;
using MalSearcher.Model.MalProxy.DatabaseGetters;

namespace MalSearcher.Model.MalProxy
{
    public class DatabaseManager
    {
        private Searcher mSearcher;
        private AnimeListRetriever mAnimeListRetriever;
        private CharacterRetriever mCharacterRetriever;
        private DatabaseConsolidator mDatabaseConsolidator;
        private string mDbPath;

        public User User { get; private set; }
        public ICollection<MyAnimeEntry> AnimeEntries => User.AnimeList;
        
        public DatabaseManager(string dbPath, Searcher searcher, AnimeListRetriever animeListRetriever, CharacterRetriever characterListRetriever)
        {
            mSearcher = searcher;
            mAnimeListRetriever = animeListRetriever;
            mCharacterRetriever = characterListRetriever;
            mDatabaseConsolidator = new DatabaseConsolidator();
            mDbPath = dbPath;
        }

        public IEnumerable<CharacterActor> CrossReference(Character selectedCharacter, Language selectedLanguage)
        {
            var actor = selectedCharacter.Actors.Where(a => a.Language.Equals(selectedLanguage)).FirstOrDefault()?.Actor;
            IEnumerable<CharacterActor> ret;
            using (var context = new AnimeDbContext(mDbPath))
            {
                var animeIds = User.AnimeList.Select(a => a.AnimeId);
                ret = (from animeId in User.AnimeList.Select(a => a.AnimeId)
                      join characterAnime in context.CharacterAnime on animeId equals characterAnime.AnimeId
                      join characterActor in context.CharacterActors
                        .Include(ca => ca.Character)
                            .ThenInclude(c => c.CharacterAnimes)
                            .ThenInclude(ca => ca.Anime)
                        .Include(ca => ca.Character)
                            .ThenInclude(c => c.Name)
                        .Include(ca => ca.Actor)
                            .ThenInclude(a => a.Name)
                      on characterAnime.CharacterId equals characterActor.CharacterId
                      where characterActor.ActorId == actor.VoiceActorID
                      select characterActor).Distinct().ToList();
            }
            return ret;
        }

        public void RefreshAnimeList(string username)
        {
            User = mAnimeListRetriever.Get(username);
            using (var context = new AnimeDbContext(mDbPath))
            {
                context.Database.EnsureCreated();
                mDatabaseConsolidator.ConsolidateUser(User, context);
                context.SaveChanges();
            }
            foreach (var anime in User.AnimeList.Select(a => a.Anime))
                UpdateCharactersForAnime(anime);
        }

        public Anime UpdateCharactersForAnime(Anime anime)
        {
            Anime dbAnime;
            using (var context = new AnimeDbContext(mDbPath))
            {
                dbAnime = context.Anime
                    .Include(a => a.CharacterAnimes)
                        .ThenInclude(ca => ca.Character)
                        .ThenInclude(c => c.Name)
                    .Where(a => a.AnimeID == anime.AnimeID).FirstOrDefault();

                if (dbAnime != null)
                    context.CopyItem(dbAnime, anime);
                if (dbAnime == null || dbAnime.Characters.Count() == 0)
                    forceUpdateCharactersForAnime(anime, context);
                context.SaveChanges();
            }
            return dbAnime;
        }

        internal IEnumerable<Language> GetLanguages(Character selectedCharacter)
        {
            if (selectedCharacter == null) return new List<Language>();
            using (var context = new AnimeDbContext(mDbPath))
            {
                var dbCharacter = CharacterDbGetter.Instance.GetFromDatabase(context, selectedCharacter);
                context.Entry(dbCharacter).Collection(c => c.Actors).Load();
                foreach (var characterActor in dbCharacter.Actors)
                    context.Entry(characterActor).Reference(ca => ca.Language).Load();
                return dbCharacter.Actors.Select(a => a.Language).Distinct();
            }
        }

        internal Character IncludeCharacterValues(Character character)
        {
            if (character == null) return null;
            using (var context = new AnimeDbContext(mDbPath))
            {
                var dbCharacter = CharacterDbGetter.Instance.GetFromDatabase(context, character);
                context.Entry(dbCharacter).Collection(c => c.Actors).Load();
                foreach (var characterActor in dbCharacter.Actors)
                {
                    context.Entry(characterActor).Reference(ca => ca.Language).Load();
                    context.Entry(characterActor).Reference(ca => ca.Actor).Load();
                }
                return dbCharacter;
            }
        }

        public ICollection<Anime> Search(string searchString)
        {
            return mSearcher.Search(searchString);
        }

        private void forceUpdateCharactersForAnime(Anime anime, AnimeDbContext context)
        {
            ICollection<Character> characters = mCharacterRetriever.Get(anime);
            mDatabaseConsolidator.ConsolidateCharacters(anime, characters, context);
        }
    }
}
