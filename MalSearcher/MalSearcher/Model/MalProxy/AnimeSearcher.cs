using Database.Objects;
using MalSearcher.Model.MalProxy.Interfaces;
using MalSearcher.Model.MalProxy.Parsers;
using MalSearcher.Model.MalProxy.Retrievers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MalSearcher.Model.MalProxy
{
    class AnimeSearcher : Searcher
    {
        private const string URL = "https://myanimelist.net/api/anime/search.xml?q={0}";

        private BaseWebRetriever<ICollection<Anime>> mRetriever;
        private string mUsername;
        private string mPassword;

        public AnimeSearcher(string username, string password)
        {
            mRetriever = new BaseWebRetriever<ICollection<Anime>>(new SearchResultParser());
            mUsername = username;
            mPassword = password;
        }

        public ICollection<Anime> Search(string query)
        {
            try
            {
                string url = String.Format(URL, query.Replace(' ', '+'));
                return mRetriever.Get(url, mUsername, mPassword);
            }
            catch
            {
                return new List<Anime>();
            }
        }
    }
}
