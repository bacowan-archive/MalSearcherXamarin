using MalSearcher.Model.MalProxy.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Objects;

namespace MalSearcher.Model.MalProxy.Retrievers
{
    class MockCharacterRetriever : CharacterRetriever
    {
        public ICollection<Character> Get(Anime anime)
        {
            throw new NotImplementedException();
        }
    }
}
