using Database.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    /*class AnimeConsolidator
    {
        public void Consolidate(IEnumerable<Anime> source, DbSet<Anime> destination)
        {
            var animePairs = from src in source
                             join dest in destination
                            on src.AnimeID equals dest.AnimeID into destGroup
                            from pair in destGroup.DefaultIfEmpty()
                             select new { Src = src, Dest = pair };

            foreach (var pair in animePairs)
            {
                if (pair.Dest == null)
                    destination.Add(pair.Src);
                else
                    pair.Dest.Join(pair.Src);
            }
        }
    }*/
}
