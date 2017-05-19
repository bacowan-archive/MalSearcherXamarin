using Database.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MalSearcher.Model.MalProxy.Interfaces
{
    public interface AnimeListRetriever
    {
        User Get(string username);
    }
}
