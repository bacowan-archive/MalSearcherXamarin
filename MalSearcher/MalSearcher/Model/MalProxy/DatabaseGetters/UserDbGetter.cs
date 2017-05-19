using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;
using Database.Objects;

namespace MalSearcher.Model.MalProxy.DatabaseGetters
{
    internal class UserDbGetter : DatabaseGetter<User>
    {
        public static UserDbGetter Instance = new UserDbGetter();

        public void AddToDatabase(AnimeDbContext context, User item)
        {
            context.Users.Add(item);
        }

        public User GetFromDatabase(AnimeDbContext context, User item)
        {
            return item?.UserId != null ? context.Users.Find(item.UserId) : null;
        }
    }
}
