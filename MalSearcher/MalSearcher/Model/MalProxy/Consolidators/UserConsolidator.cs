using Database.Context;
using Database.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MalSearcher.Model.MalProxy.Consolidators
{
    internal class UserConsolidator : BaseConsolidator<User>
    {

        protected override User getFromDatabase(AnimeDbContext context, User item)
        {
            return context.Users.Find(item.UserId);
        }
    }
}
