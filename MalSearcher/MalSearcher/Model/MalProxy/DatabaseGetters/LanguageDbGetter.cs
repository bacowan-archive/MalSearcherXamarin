using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;
using Database.Objects;

namespace MalSearcher.Model.MalProxy.DatabaseGetters
{
    internal class LanguageDbGetter : DatabaseGetter<Language>
    {
        public static LanguageDbGetter Instance = new LanguageDbGetter();

        public void AddToDatabase(AnimeDbContext context, Language item)
        {
            context.Languages.Add(item);
        }

        public Language GetFromDatabase(AnimeDbContext context, Language item)
        {
            return item?.LanguageID != null ? context.Languages.Find(item.LanguageID) : null;
        }
    }
}
