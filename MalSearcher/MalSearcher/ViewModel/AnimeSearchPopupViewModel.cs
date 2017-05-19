using Database.Objects;
using MalSearcher.Model.MalProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace MalSearcher.ViewModel
{
    class AnimeSearchPopupViewModel : BaseViewModel
    {
        private DatabaseManager mDbManager;

        public ICollection<Anime> List { get; set; }
        private string mSearchText;
        public string SearchText
        {
            get { return mSearchText; }
            set
            {
                mSearchText = value;
                OnPropertyChanged();
            }
        }

        public AnimeSearchPopupViewModel() { }

        public AnimeSearchPopupViewModel(DatabaseManager dbManager)
        {
            mDbManager = dbManager;
        }

        internal void Search()
        {
            List = mDbManager.Search(SearchText);
            OnPropertyChanged("List");
        }
    }
}
