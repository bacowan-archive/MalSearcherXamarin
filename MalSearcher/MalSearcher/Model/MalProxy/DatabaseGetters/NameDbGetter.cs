using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;
using Database.Objects;

namespace MalSearcher.Model.MalProxy.DatabaseGetters
{
    internal class NameDbGetter : DatabaseGetter<Name>
    {
        public static NameDbGetter Instance = new NameDbGetter();

        public void AddToDatabase(AnimeDbContext context, Name item)
        {
            context.Names.Add(item);
        }

        public Name GetFromDatabase(AnimeDbContext context, Name item)
        {
            return item?.NameId != null ? context.Names.Find(item.NameId) : null;
        }
    }
}
