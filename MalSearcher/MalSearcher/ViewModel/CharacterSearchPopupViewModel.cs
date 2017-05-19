using System;
using System.Collections.Generic;
using System.Text;
using MalSearcher.Model.MalProxy;
using Database.Objects;

namespace MalSearcher.ViewModel
{
    class CharacterSearchPopupViewModel : BaseViewModel
    {
        private DatabaseManager mDbManager;
        private Anime mAnime;

        public CharacterSearchPopupViewModel(DatabaseManager dbManager, Anime anime)
        {
            mDbManager = dbManager;
            mAnime = anime;
        }

        public IEnumerable<Character> List => mAnime.Characters;
    }
}
