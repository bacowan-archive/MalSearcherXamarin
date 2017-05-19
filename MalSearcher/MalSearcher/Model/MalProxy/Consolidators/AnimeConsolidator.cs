using Database.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    class AnimeConsolidator : BaseConsolidator<Anime>
    {
        protected override Anime getFromDatabase(AnimeDbContext context, Anime item)
        {
            return context.Anime.Find(item.AnimeID);
        }
    }
}
