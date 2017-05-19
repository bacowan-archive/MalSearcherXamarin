using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MalSearcher.View
{
	public partial class App : Application
	{
        public App() { }

		public App (string filePath, string dbPath)
		{
			InitializeComponent();

			MainPage = new NavigationPage(new LoginPage(filePath, dbPath));
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
