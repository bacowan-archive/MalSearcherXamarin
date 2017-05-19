using Database.Context;
using Database.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    /*internal class AnimeListConsolidator : IAnimeListConsolidator
    {
        public void Consolidate(HashSet<MyAnimeEntry> animeList, AnimeDbContext context)
        {
            foreach (var entry in animeList)
            {
                MyAnimeEntry dbEntry = context.MyAnimeEntries.Find(entry.AnimeId, entry.UserId);
                Consolidate(dbEntry, entry);
                animeList.Add(dbEntry);

            }
        }

        public void Consolidate(MyAnimeEntry databaseEntry, MyAnimeEntry pulledEntry)
        {
            databaseEntry.AnimeId = pulledEntry.AnimeId;
            databaseEntry.UserId = pulledEntry.UserId;
            databaseEntry.User = pulledEntry.User;
            databaseEntry.Anime = pulledEntry.Anime;
            databaseEntry.MyIdValue = pulledEntry.MyIdValue;
            databaseEntry.WatchedEpisodes = pulledEntry.WatchedEpisodes;
            databaseEntry.StartDate = pulledEntry.StartDate;
            databaseEntry.EndDate = pulledEntry.EndDate;
            databaseEntry.Score = pulledEntry.Score;
            databaseEntry.Status = pulledEntry.Status;
            databaseEntry.RewatchedEpisodes = pulledEntry.RewatchedEpisodes;
            databaseEntry.RewatchValue = pulledEntry.RewatchValue;
            databaseEntry.LastUpdated = pulledEntry.LastUpdated;
            pulledEntry = databaseEntry;
        }
    }*/
}
