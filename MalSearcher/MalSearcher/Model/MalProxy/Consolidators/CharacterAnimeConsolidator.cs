using Database.Objects.Intersections;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    class CharacterAnimeConsolidator : BaseConsolidator<CharacterAnime>
    {
        protected override CharacterAnime getFromDatabase(AnimeDbContext context, CharacterAnime item)
        {
            return context.CharacterAnime.Find(item.CharacterId, item.AnimeId);
        }
    }
}
