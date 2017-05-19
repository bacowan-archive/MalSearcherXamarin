using Database.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    class CharacterConsolidator : BaseConsolidator<Character>
    {
        protected override Character getFromDatabase(AnimeDbContext context, Character item)
        {
            return context.Characters.Find(item.CharacterID);
        }
    }
}
