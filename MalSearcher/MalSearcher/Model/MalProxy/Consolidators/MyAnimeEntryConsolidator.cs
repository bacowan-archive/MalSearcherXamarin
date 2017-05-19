using Database.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    class MyAnimeEntryConsolidator : BaseConsolidator<MyAnimeEntry>
    {

        protected override MyAnimeEntry getFromDatabase(AnimeDbContext context, MyAnimeEntry item)
        {
            return context.MyAnimeEntries.Find(item.AnimeId, item.UserId);
        }
    }
}
