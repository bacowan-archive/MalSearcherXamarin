using Database.Context;
using Database.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    public abstract class BaseConsolidator<T>
        where T : class, DatabaseObject<T>
    {
        public void Consolidate(IEnumerable<T> items, AnimeDbContext context)
        {
            foreach (T item in items)
                Consolidate(item, context);
        }

        public T Consolidate(T item, AnimeDbContext context)
        {
            T dbItem = getFromDatabase(context, item);
            if (dbItem != null)
            {
                dbItem.CopyFrom(item);
                return dbItem;
            }
            else
                return item;
        }

        protected abstract T getFromDatabase(AnimeDbContext context, T item);
    }
}
