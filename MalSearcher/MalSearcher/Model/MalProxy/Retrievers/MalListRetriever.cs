using MalSearcher.Model.MalProxy.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Objects;
using MalSearcher.Model.MalProxy.Parsers;

namespace MalSearcher.Model.MalProxy.Retrievers
{
    class MalListRetriever : AnimeListRetriever
    {
        private const string URL = "https://myanimelist.net/malappinfo.php?u={0}&status=all&type=anime";

        private BaseWebRetriever<User> mRetriever;

        public MalListRetriever(UserAnimeListParser listParser)
        {
            mRetriever = new BaseWebRetriever<User>(listParser);
        }

        public User Get(string username)
        {
            return mRetriever.Get(String.Format(URL, username));
        }
    }
}
