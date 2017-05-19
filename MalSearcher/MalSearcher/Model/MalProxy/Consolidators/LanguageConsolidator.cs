using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;
using Database.Objects;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    internal class LanguageConsolidator : BaseConsolidator<Language>
    {
        protected override Language getFromDatabase(AnimeDbContext context, Language item)
        {
            return context.Languages.Find(item.LanguageID);
        }
    }
}
