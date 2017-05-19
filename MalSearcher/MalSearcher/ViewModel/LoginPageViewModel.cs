using MalSearcher.Model.MalProxy.Retrievers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace MalSearcher.ViewModel
{
    class LoginPageViewModel : BaseViewModel
    {
        private const string PASSWORD_FILE_NAME = "password";
        private const string USERNAME_FILE_NAME = "username";

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

        private string mUsernameFilePath;
        private string mPasswordFilePath;

        internal void SaveCredentials()
        {
            File.WriteAllText(mUsernameFilePath, Username);
            File.WriteAllText(mPasswordFilePath, Password);
        }

        public bool AreCredentialsValid => mCredentialValidator.Get(Username, Password);

        private CredentialValidator mCredentialValidator;

        public LoginPageViewModel(string filePath)
        {
            mCredentialValidator = new CredentialValidator();
            mUsernameFilePath = Path.Combine(filePath, USERNAME_FILE_NAME);
            mPasswordFilePath = Path.Combine(filePath, PASSWORD_FILE_NAME);
            try
            {
                Username = File.ReadAllText(mUsernameFilePath);
                Password = File.ReadAllText(mPasswordFilePath);
            }
            catch (FileNotFoundException)
            {
                Username = "";
                Password = "";
            }
        }
    }
}
