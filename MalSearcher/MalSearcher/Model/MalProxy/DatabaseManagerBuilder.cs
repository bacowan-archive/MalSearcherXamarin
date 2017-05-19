using MalSearcher.Model.MalProxy.Consolidators;
using MalSearcher.Model.MalProxy.Parsers;
using MalSearcher.Model.MalProxy.Retrievers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MalSearcher.Model.MalProxy
{
    class DatabaseManagerBuilder
    {
        public static DatabaseManager BuildDefaultDatabaseManager(string dbPath, string username, string password)
        {
            return new DatabaseManager(
                dbPath,
                new AnimeSearcher(username, password), 
               new MalListRetriever(new UserAnimeListParser()),
               new MalCharacterRetriever());
        }

        internal static DatabaseManager BuildMockDatabaseManager(string dbPath, string username, string password)
        {
            return new DatabaseManager(
                dbPath,
                new AnimeSearcher(username, password),
                new MockListRetriever(),
                new MockCharacterRetriever());
        }
    }
}
