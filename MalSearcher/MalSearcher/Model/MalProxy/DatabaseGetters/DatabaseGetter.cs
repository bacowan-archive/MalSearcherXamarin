using Database.Context;
using Database.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MalSearcher.Model.MalProxy.DatabaseGetters
{
    internal interface DatabaseGetter<T>
    {
        T GetFromDatabase(AnimeDbContext context, T item);
        void AddToDatabase(AnimeDbContext context, T item);
    }
}
