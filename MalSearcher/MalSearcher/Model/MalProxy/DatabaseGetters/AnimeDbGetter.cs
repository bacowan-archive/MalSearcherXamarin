using Database.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;
using Microsoft.EntityFrameworkCore;

namespace MalSearcher.Model.MalProxy.DatabaseGetters
{
    internal class AnimeDbGetter : DatabaseGetter<Anime>
    {
        public static AnimeDbGetter Instance = new AnimeDbGetter();

        public void AddToDatabase(AnimeDbContext context, Anime item)
        {
            context.Anime.Add(item);
        }

        public Anime GetFromDatabase(AnimeDbContext context, Anime item)
        {
            return item?.AnimeID != null ? context.Anime.Find(item.AnimeID) : null;
        }
    }
}
