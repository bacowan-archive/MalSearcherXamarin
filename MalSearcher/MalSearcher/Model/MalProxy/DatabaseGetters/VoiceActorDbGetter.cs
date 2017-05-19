using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;
using Database.Objects;

namespace MalSearcher.Model.MalProxy.DatabaseGetters
{
    internal class VoiceActorDbGetter : DatabaseGetter<VoiceActor>
    {
        public static VoiceActorDbGetter Instance = new VoiceActorDbGetter();

        public void AddToDatabase(AnimeDbContext context, VoiceActor item)
        {
            context.VoiceActors.Add(item);
        }

        public VoiceActor GetFromDatabase(AnimeDbContext context, VoiceActor item)
        {
            return item?.VoiceActorID != null ? context.VoiceActors.Find(item.VoiceActorID) : null;
        }
    }
}
