using Database.Objects.Intersections;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;

namespace MalSearcher.Model.MalProxy.DatabaseGetters
{
    internal class CharacterAnimeDbGetter : DatabaseGetter<CharacterAnime>
    {
        public static CharacterAnimeDbGetter Instance = new CharacterAnimeDbGetter();

        public void AddToDatabase(AnimeDbContext context, CharacterAnime item)
        {
            context.CharacterAnime.Add(item);
        }

        public CharacterAnime GetFromDatabase(AnimeDbContext context, CharacterAnime item)
        {
            return item?.CharacterId != null && item?.AnimeId != null ? context.CharacterAnime.Find(item.CharacterId, item.AnimeId) : null;
        }
    }
}
