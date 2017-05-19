using Database.Context;
using Database.Objects;
using System.Linq;
using System;
using System.Collections.Generic;
using Database.Objects.Intersections;
using MalSearcher.Model.MalProxy.DatabaseGetters;
using Microsoft.EntityFrameworkCore;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    internal class DatabaseConsolidator
    {
        private UserConsolidator mUserConsolidator;
        private MyAnimeEntryConsolidator mAnimeEntryConsolidator;
        private AnimeConsolidator mAnimeConsolidator;
        private CharacterConsolidator mCharacterConsolidator;
        private VoiceActorConsolidator mVoiceActorConsolidator;
        private CharacterActorConsolidator mCharacterActorConsolidator;
        private CharacterAnimeConsolidator mCharacterAnimeConsolidator;
        private LanguageConsolidator mLanguageConsolidator;
        private NameConsolidator mNameConsolidator;


        public DatabaseConsolidator()
        {
            mUserConsolidator = new UserConsolidator();
            mAnimeEntryConsolidator = new MyAnimeEntryConsolidator();
            mAnimeConsolidator = new AnimeConsolidator();
            mCharacterConsolidator = new CharacterConsolidator();
            mVoiceActorConsolidator = new VoiceActorConsolidator();
            mCharacterConsolidator = new CharacterConsolidator();
            mCharacterActorConsolidator = new CharacterActorConsolidator();
            mCharacterAnimeConsolidator = new CharacterAnimeConsolidator();
            mLanguageConsolidator = new LanguageConsolidator();
            mNameConsolidator = new NameConsolidator();
        }

        public void ConsolidateUser(User user, AnimeDbContext context)
        {
            var dbUser = context.Users.Find(user.UserId);
            User addedUser;
            if (dbUser != null)
            {
                context.CopyItem(dbUser, user);
                addedUser = dbUser;
            }
            else
            {
                context.Add(user);
                addedUser = user;
            }

            foreach (var animeEntry in addedUser.AnimeList)
            {
                var dbAnimeEntry = context.MyAnimeEntries.Find(animeEntry.AnimeId, animeEntry.UserId);
                MyAnimeEntry addedAnimeEntry;
                if (dbAnimeEntry != null)
                {
                    context.CopyItem(dbAnimeEntry, animeEntry);
                    addedAnimeEntry = dbAnimeEntry;
                }
                else
                {
                    context.Add(animeEntry);
                    addedAnimeEntry = animeEntry;
                }

                var dbAnime = context.Anime.Find(addedAnimeEntry.Anime.AnimeID);
                if (dbAnime != null)
                    context.CopyItem(dbAnime, addedAnimeEntry.Anime);
                else
                    context.Add(addedAnimeEntry.Anime);
            }
        }

        internal void ConsolidateCharacters(Anime anime, ICollection<Character> characters, AnimeDbContext context)
        {
            //var initialTrackingBehavior = context.ChangeTracker.QueryTrackingBehavior;
            //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var characterAnimes = characters.SelectMany(c => c.CharacterAnimes).Distinct(CharacterAnimeHashSet.Comparer);
            var characterActors = characters.SelectMany(c => c.Actors).Distinct(CharacterActorHashSet.Comparer);
            var voiceActors = characterActors.Select(c => c.Actor).Distinct(VoiceActorHashSet.Comparer);
            var languages = characterActors.Select(c => c.Language).Distinct(LanguageHashSet.Comparer);
            var names = characters.Select(c => c.Name).Union(voiceActors.Select(v => v.Name)).Distinct(NameHashSet.Comparer);

            Anime selectedAnime;
            Anime dbAnime = AnimeDbGetter.Instance.GetFromDatabase(context, anime);
            if (dbAnime != null)
            {
                selectedAnime = dbAnime;
                context.Entry(selectedAnime).Collection(a => a.CharacterAnimes).Load();
            }
            else
                selectedAnime = anime;

            // update links
            foreach (var characterAnime in characterAnimes)
            {
                CharacterAnime selectedCharacterAnime;
                var dbCharacterAnime = CharacterAnimeDbGetter.Instance.GetFromDatabase(context, characterAnime);
                if (dbCharacterAnime != null)
                    selectedCharacterAnime = dbCharacterAnime;
                else
                    selectedCharacterAnime = characterAnime;

                Character selectedCharacter;
                var dbCharacter = CharacterDbGetter.Instance.GetFromDatabase(context, characterAnime.Character);
                List<CharacterActor> actorLoop;
                if (dbCharacter != null)
                {
                    selectedCharacter = dbCharacter;
                    context.Entry(selectedCharacter).Reference(c => c.Name).Load();
                    context.Entry(selectedCharacter).Collection(c => c.CharacterAnimes).Load();
                    context.Entry(selectedCharacter).Collection(c => c.Actors).Load();
                    actorLoop = selectedCharacter.Actors.ToList();
                }
                else
                {
                    selectedCharacter = characterAnime.Character;
                    actorLoop = selectedCharacter.Actors.ToList();
                    selectedCharacter.Actors.Clear();
                }

                Name selectedName;
                var dbName = NameDbGetter.Instance.GetFromDatabase(context, selectedCharacter.Name);
                if (dbName != null)
                    selectedName = dbName;
                else
                    selectedName = selectedCharacter.Name;

                //var actorLoop = selectedCharacter.Actors.ToList();
                //selectedCharacter.Actors.Clear();
                foreach (var actor in actorLoop)
                {
                    CharacterActor selectedCharacterActor;
                    var dbCharacterActor = CharacterActorDbGetter.Instance.GetFromDatabase(context, actor);
                    if (dbCharacterActor != null)
                    {
                        selectedCharacterActor = dbCharacterActor;
                        context.Entry(selectedCharacterActor).Reference(ca => ca.Character).Load();
                        context.Entry(selectedCharacterActor).Reference(ca => ca.Actor).Load();
                        context.Entry(selectedCharacterActor).Reference(ca => ca.Language).Load();
                    }
                    else
                        selectedCharacterActor = actor;

                    Language selectedLanguage;
                    var dbLanguage = LanguageDbGetter.Instance.GetFromDatabase(context, actor.Language);
                    if (dbLanguage != null)
                        selectedLanguage = dbLanguage;
                    else
                        selectedLanguage = actor.Language;

                    VoiceActor selectedVoiceActor;
                    var dbVoiceActor = VoiceActorDbGetter.Instance.GetFromDatabase(context, selectedCharacterActor.Actor);
                    if (dbVoiceActor != null)
                    {
                        selectedVoiceActor = dbVoiceActor;
                        context.Entry(selectedVoiceActor).Reference(va => va.Name).Load();
                        context.Entry(selectedVoiceActor).Collection(va => va.Characters).Load();
                    }
                    else
                        selectedVoiceActor = selectedCharacterActor.Actor;

                    Name selectedVoiceActorName;
                    var dbVoiceActorName = NameDbGetter.Instance.GetFromDatabase(context, selectedVoiceActor.Name);
                    if (dbVoiceActorName != null)
                        selectedVoiceActorName = dbVoiceActorName;
                    else
                        selectedVoiceActorName = selectedVoiceActor.Name;
                    selectedVoiceActor.Name = selectedVoiceActorName;
                    selectedVoiceActor.Characters.Add(selectedCharacterActor);
                    selectedCharacter.Actors.Add(selectedCharacterActor);
                    selectedCharacterActor.Language = selectedLanguage;
                    selectedCharacterActor.Character = selectedCharacter;
                    selectedCharacterActor.Actor = selectedVoiceActor;
                }
                selectedAnime.CharacterAnimes.Add(selectedCharacterAnime);
                selectedCharacterAnime.Anime = selectedAnime;
                selectedCharacterAnime.Character = selectedCharacter;
                selectedCharacter.Name = selectedName;
                selectedCharacter.CharacterAnimes.Add(selectedCharacterAnime);

            }
            // update regular properties
            /*foreach (var character in selectedAnime.Characters)
                context.CopyOrAddItem(character, CharacterDbGetter.Instance);
            foreach (var voiceActor in voiceActors)
                context.CopyOrAddItem(voiceActor, VoiceActorDbGetter.Instance);
            foreach (var language in languages)
                context.CopyOrAddItem(language, LanguageDbGetter.Instance);
            foreach (var name in names)
                context.CopyOrAddItem(name, NameDbGetter.Instance);
            foreach (var characterAnime in characterAnimes)
                context.CopyOrAddItem(characterAnime, CharacterAnimeDbGetter.Instance);
            foreach (var characterActor in characterActors)
                context.CopyOrAddItem(characterActor, CharacterActorDbGetter.Instance);*/
                
        }

        public User Consolidate(User user, AnimeDbContext context)
        {
            var animeEntries = user.AnimeList;
            var animes = animeEntries.Select(a => a.Anime).Distinct();
            var characterAnimes = animes.SelectMany(a => a.CharacterAnimes).Distinct();
            var characters = characterAnimes.Select(a => a.Character).Distinct();
            var characterActors = characters.SelectMany(c => c.Actors).Distinct();
            var voiceActors = characterActors.Select(c => c.Actor).Distinct();
            var languages = characterActors.Select(c => c.Language).Distinct();
            var names = characters.Select(c => c.Name).Union(voiceActors.Select(v => v.Name)).Distinct();
            
            mNameConsolidator.Consolidate(names, context);
            mLanguageConsolidator.Consolidate(languages, context);
            mVoiceActorConsolidator.Consolidate(voiceActors, context);
            mCharacterActorConsolidator.Consolidate(characterActors, context);
            mCharacterConsolidator.Consolidate(characters, context);
            mCharacterAnimeConsolidator.Consolidate(characterAnimes, context);
            mAnimeConsolidator.Consolidate(animes, context);
            mAnimeEntryConsolidator.Consolidate(animeEntries, context);
            mUserConsolidator.Consolidate(user, context);
            return user;
        }
    }

    internal static class ExtensionMethods
    {
        public static void CopyOrAddItem<T>(this AnimeDbContext context, T copy, DatabaseGetter<T> getter) where T : class, DatabaseObject<T>
        {
            var dbObject = getter.GetFromDatabase(context, copy);
            if (dbObject != null)
                context.CopyItem(dbObject, copy);
            //else
            //    getter.AddToDatabase(context, copy);
        }

        public static void CopyItem<T>(this AnimeDbContext context, T dbItem, T copy) where T : class
        {
            context.Entry(dbItem).CurrentValues.SetValues(copy);
        }

        public static void AddAll<T>(this HashSet<T> set, IEnumerable<T> items)
        {
            foreach (T item in items)
                set.Add(item);
        }
    }
}
