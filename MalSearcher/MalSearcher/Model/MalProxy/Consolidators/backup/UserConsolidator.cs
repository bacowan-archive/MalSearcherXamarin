using System;
using System.Collections.Generic;
using System.Text;
using Database.Objects;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Database.Context;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    /*class UserConsolidator
    {
        private IAnimeListConsolidator mListConsolidator;

        public UserConsolidator(IAnimeListConsolidator listConsolidator)
        {
            mListConsolidator = listConsolidator;
        }

        internal void Consolidate(User user, AnimeDbContext context)
        {
            User existingUser = context.Users.Find(user.UserId);
            //mListConsolidator.Consolidate(user.AnimeList, context);
            if (existingUser == null)
                context.Users.Add(user);
            else
                Consolidate(existingUser, ref user);
        }

        public void Consolidate(User databaseUser, ref User pulledUser)
        {
            databaseUser.Username = pulledUser.Username;
            databaseUser.Watching = pulledUser.Watching;
            databaseUser.Completed = pulledUser.Completed;
            databaseUser.OnHold = pulledUser.OnHold;
            databaseUser.PlanToWatch = pulledUser.PlanToWatch;
            databaseUser.Dropped = pulledUser.Dropped;
            databaseUser.DaysSpentWatching = pulledUser.DaysSpentWatching;
            databaseUser.AnimeList = pulledUser.AnimeList;
            pulledUser = databaseUser;
        }
    }*/
}
