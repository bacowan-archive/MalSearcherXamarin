using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Objects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MalSearcher.ViewModel;
using MalSearcher.Model.MalProxy;

namespace MalSearcher.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CrossReferencePage : ContentPage
	{
        public CrossReferencePage()
		{
			InitializeComponent();
		}

        public CrossReferencePage(DatabaseManager dbManager, Character selectedCharacter, Language selectedLanguage)
        {
            InitializeComponent();
            BindingContext = new CrossReferencePageViewModel(dbManager, selectedCharacter, selectedLanguage);
        }
    }
}
