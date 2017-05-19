using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Database.Objects;
using MalSearcher.Model.MalProxy;
using MalSearcher.ViewModel;
using Rg.Plugins.Popup.Services;

namespace MalSearcher.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacterSearchPopup : PopupPage
	{

        private Action<Character> mSetCharacterCallback;

        public CharacterSearchPopup() : this(null, null, null) { }

        public CharacterSearchPopup(DatabaseManager dbManager, Anime anime, Action<Character> setCharacterCallback)
        {
            BindingContext = new CharacterSearchPopupViewModel(dbManager, anime);
            mSetCharacterCallback = setCharacterCallback;
            InitializeComponent();
        }

        public void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            PopupNavigation.RemovePageAsync(this);
            mSetCharacterCallback.Invoke(e.SelectedItem as Character);
        }
    }
}
