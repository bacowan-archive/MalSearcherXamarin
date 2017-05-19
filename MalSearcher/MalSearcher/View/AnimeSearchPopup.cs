using Database.Objects;
using MalSearcher.Model.MalProxy;
using MalSearcher.ViewModel;
using Rg.Plugins.Popup.Pages;
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
	public partial class AnimeSearchPopup : PopupPage
	{
        private AnimeSearchPopupViewModel mCastedViewModel => BindingContext as AnimeSearchPopupViewModel;

        private Action<Anime> mSetAnimeCallback;

        public AnimeSearchPopup() : this(null, null) { }

		public AnimeSearchPopup(DatabaseManager dbManager, Action<Anime> setAnimeCallback)
		{
			InitializeComponent ();
            BindingContext = new AnimeSearchPopupViewModel(dbManager);
            mSetAnimeCallback = setAnimeCallback;
		}

        public void SearchButtonClicked(object sender, EventArgs e)
        {
            mCastedViewModel.Search();
        }

        public void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            PopupNavigation.RemovePageAsync(this);
            mSetAnimeCallback.Invoke(e.SelectedItem as Anime);
        }

    }
}
