using Database.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MalSearcher.Model.MalProxy.Interfaces
{
    public interface Searcher
    {
        ICollection<Anime> Search(string query);
    }
}
