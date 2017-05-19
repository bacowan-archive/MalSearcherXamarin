using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;
using Database.Objects.Intersections;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    class CharacterActorConsolidator : BaseConsolidator<CharacterActor>
    {

        protected override CharacterActor getFromDatabase(AnimeDbContext context, CharacterActor item)
        {
            return context.CharacterActors.Find(item.CharacterId, item.ActorId);
        }
    }
}
