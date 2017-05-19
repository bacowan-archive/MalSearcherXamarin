using System;
using System.Collections.Generic;
using System.Text;
using MalSearcher.Model.MalProxy;
using Database.Objects;

namespace MalSearcher.ViewModel
{
    class ListPageViewModel : BaseViewModel
    {
        private DatabaseManager mDbManager;

        public ICollection<MyAnimeEntry> List => mDbManager.AnimeEntries;

        public ListPageViewModel(DatabaseManager dbManager)
        {
            mDbManager = dbManager;
        }
    }
}
