using Database.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    class VoiceActorConsolidator : BaseConsolidator<VoiceActor>
    {
        protected override VoiceActor getFromDatabase(AnimeDbContext context, VoiceActor item)
        {
            return context.VoiceActors.Find(item.VoiceActorID);
        }
    }
}
