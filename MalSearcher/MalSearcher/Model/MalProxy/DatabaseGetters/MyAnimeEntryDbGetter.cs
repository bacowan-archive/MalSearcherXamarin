using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;
using Database.Objects;

namespace MalSearcher.Model.MalProxy.DatabaseGetters
{
    internal class MyAnimeEntryDbGetter : DatabaseGetter<MyAnimeEntry>
    {
        public static MyAnimeEntryDbGetter Instance = new MyAnimeEntryDbGetter();

        public void AddToDatabase(AnimeDbContext context, MyAnimeEntry item)
        {
            context.MyAnimeEntries.Add(item);
        }

        public MyAnimeEntry GetFromDatabase(AnimeDbContext context, MyAnimeEntry item)
        {
            return item?.AnimeId != null && item?.UserId != null ? context.MyAnimeEntries.Find(item.AnimeId, item.UserId) : null;
        }
    }
}
