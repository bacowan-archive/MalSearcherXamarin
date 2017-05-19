using MalSearcher.Model.MalProxy.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Objects;

namespace MalSearcher.Model.MalProxy.Retrievers
{
    class MockListRetriever : AnimeListRetriever
    {
        public User Get(string username)
        {
            return new User()
            {
                UserId = 1,
                Username = username,
                Watching = 1,
                Completed = 2,
                OnHold = 3,
                Dropped = 4,
                PlanToWatch = 5,
                DaysSpentWatching = 6,
            };
        }
    }
}
