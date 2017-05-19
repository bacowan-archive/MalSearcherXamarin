using Database.Objects.Intersections;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;

namespace MalSearcher.Model.MalProxy.DatabaseGetters
{
    internal class CharacterActorDbGetter : DatabaseGetter<CharacterActor>
    {
        public static CharacterActorDbGetter Instance = new CharacterActorDbGetter();

        public void AddToDatabase(AnimeDbContext context, CharacterActor item)
        {
            context.CharacterActors.Add(item);
        }

        public CharacterActor GetFromDatabase(AnimeDbContext context, CharacterActor item)
        {
            return item?.CharacterId != null && item?.ActorId != null && item?.LanguageId != null ?
                context.CharacterActors.Find(item.CharacterId, item.ActorId, item.LanguageId) : null;
        }
    }
}
