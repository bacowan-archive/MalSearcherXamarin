using Database.Objects;
using MalSearcher.Model.MalProxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MalSearcher.ViewModel
{
    class MainSearchPageViewModel : BaseViewModel
    {
        private const string DEFAULT_SELECT_ANIME_TEXT = "Search Anime";
        private const string DEFAULT_SELECT_CHARACTER_TEXT = "Search Character";

        private DatabaseManager mDbManager;
        public DatabaseManager DbManager => mDbManager;

        public MainSearchPageViewModel(string dbPath, string username, string password)
        {
            mDbManager = DatabaseManagerBuilder.BuildDefaultDatabaseManager(dbPath, username, password);
            mDbManager.RefreshAnimeList(username);
        }

        private string mUsername;
        public string Username
        {
            get { return mUsername; }
            set
            {
                mUsername = value;
                OnPropertyChanged();
            }
        }

        private string mPassword;
        public string Password
        {
            get { return mPassword; }
            set
            {
                mPassword = value;
                OnPropertyChanged();
            }
        }

        private Anime mSelectedAnime;
        public Anime SelectedAnime
        {
            get { return mSelectedAnime; }
            set
            {
                mSelectedAnime = mDbManager.UpdateCharactersForAnime(value);
                OnPropertyChanged();
                OnPropertyChanged("SelectedAnimeTitle");
                OnPropertyChanged("CharacterList");
                OnPropertyChanged("HasSelectedAnime");
            }
        }

        public string SelectedAnimeTitle => SelectedAnime?.Title ?? DEFAULT_SELECT_ANIME_TEXT;

        public bool HasSelectedAnime => SelectedAnime != null;

        public ObservableCollection<Character> CharacterList => SelectedAnime != null ? 
            new ObservableCollection<Character>(SelectedAnime.Characters) :
            new ObservableCollection<Character>();

        private Character mSelectedCharacter;
        public Character SelectedCharacter
        {
            get { return mSelectedCharacter; }
            set
            {
                mSelectedCharacter = mDbManager.IncludeCharacterValues(value);
                OnPropertyChanged();
                OnPropertyChanged("SelectedCharacterTitle");
                OnPropertyChanged("LanguageList");
            }
        }

        public ObservableCollection<Language> LanguageList => SelectedCharacter != null ? 
            new ObservableCollection<Language>(SelectedCharacter.Actors.Select(a => a.Language)) :
            new ObservableCollection<Language>();

        private Language mSelectedLanguage;
        public Language SelectedLanguage
        {
            get { return mSelectedLanguage; }
            set
            {
                mSelectedLanguage = value;
                OnPropertyChanged();
            }
        }

        public string SelectedCharacterTitle => SelectedCharacter?.Name.English ?? DEFAULT_SELECT_CHARACTER_TEXT;

        public ObservableCollection<MyAnimeEntry> AnimeList => new ObservableCollection<MyAnimeEntry>(mDbManager.AnimeEntries);

        public void UpdateList()
        {
            mDbManager.RefreshAnimeList(Username);
            OnPropertyChanged("AnimeList");
        }
    }
}
