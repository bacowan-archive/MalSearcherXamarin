using Database.Context;
using Database.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    internal class NameConsolidator : BaseConsolidator<Name>
    {
        protected override Name getFromDatabase(AnimeDbContext context, Name item)
        {
            return context.Names.Find(item.NameId);
        }
    }
}
