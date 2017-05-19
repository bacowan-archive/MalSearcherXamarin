using Database.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MalSearcher.Model.MalProxy.Interfaces
{
    public interface CharacterRetriever
    {
        ICollection<Character> Get(Anime anime);
    }
}
