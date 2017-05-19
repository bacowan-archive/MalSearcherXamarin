using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MalSearcher.Model.MalProxy;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MalSearcher.ViewModel;

namespace MalSearcher.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListPage : ContentPage
	{
        private ListPageViewModel mCastedViewModel => BindingContext as ListPageViewModel;

        public ListPage() { }

        public ListPage(DatabaseManager dbManager)
        {
            InitializeComponent();
            BindingContext = new ListPageViewModel(dbManager);
        }
    }
}
