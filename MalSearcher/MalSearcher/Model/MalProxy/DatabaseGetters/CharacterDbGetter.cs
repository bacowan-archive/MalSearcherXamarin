using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;
using Database.Objects;

namespace MalSearcher.Model.MalProxy.DatabaseGetters
{
    internal class CharacterDbGetter : DatabaseGetter<Character>
    {
        public static CharacterDbGetter Instance = new CharacterDbGetter();

        public void AddToDatabase(AnimeDbContext context, Character item)
        {
            context.Characters.Add(item);
        }

        public Character GetFromDatabase(AnimeDbContext context, Character item)
        {
            return item?.CharacterID != null ? context.Characters.Find(item.CharacterID) : null;
        }
    }
}
