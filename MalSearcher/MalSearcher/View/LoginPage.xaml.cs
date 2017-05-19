using MalSearcher.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MalSearcher.View
{
	public partial class LoginPage : ContentPage
	{
        private LoginPageViewModel mCastedViewModel => BindingContext as LoginPageViewModel;
        private string mDbPath;
        private string mFilePath;

        public LoginPage() { InitializeComponent(); }

		public LoginPage(string filePath, string dbPath)
		{
			InitializeComponent();
            BindingContext = new LoginPageViewModel(filePath);
            mDbPath = dbPath;
            mFilePath = filePath;
		}

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (mCastedViewModel?.AreCredentialsValid == true)
            {
                if (SaveCredentialsSwitch.IsToggled)
                    mCastedViewModel.SaveCredentials();
                Navigation.PushAsync(new MainSearchPage(mDbPath, mCastedViewModel.Username, mCastedViewModel.Password));
            }
            else
            {
                displayInvalidCredentials();
            }
        }

        private void displayInvalidCredentials()
        {
            DisplayAlert("Invalid Credentials", "Please try again.", "OK");
        }
    }
}
