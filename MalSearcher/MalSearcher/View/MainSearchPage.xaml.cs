using Database.Objects;
using MalSearcher.ViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MalSearcher.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainSearchPage : ContentPage
	{

        private MainSearchPageViewModel mCastedViewModel => BindingContext as MainSearchPageViewModel;

        public MainSearchPage() { InitializeComponent(); }

		public MainSearchPage(String dbPath, String username, String password)
		{
			InitializeComponent ();
            BindingContext = new MainSearchPageViewModel(dbPath, username, password);
            mCastedViewModel.Username = username;
            mCastedViewModel.Password = password;
		}

        private void list_button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListPage(mCastedViewModel.DbManager));
        }

        private void search_anime_button_clicked(object sender, EventArgs e)
        {
            AnimeSearchPopup popup = new AnimeSearchPopup(mCastedViewModel.DbManager, setAnimeCallback);
            PopupNavigation.PushAsync(popup);
        }

        private void go_button_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CrossReferencePage(
                mCastedViewModel?.DbManager,
                mCastedViewModel?.SelectedCharacter,
                mCastedViewModel?.SelectedLanguage));
        }

        private void setAnimeCallback(Anime anime)
        {
            mCastedViewModel.SelectedAnime = anime;
        }
    }
}
