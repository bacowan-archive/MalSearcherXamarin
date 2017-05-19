using MalSearcher.Model.MalProxy.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MalSearcher.Model.MalProxy.Retrievers
{
    public class CredentialValidator
    {
        private const string URL = "https://myanimelist.net/api/account/verify_credentials.xml";

        private BaseWebRetriever<bool> mRetriever;

        public CredentialValidator()
        {
            mRetriever = new BaseWebRetriever<bool>(new CredentialParser());
        }

        public bool Get(string username, string password)
        {
            try
            {
                return mRetriever.Get(URL, username, password);
            }
            catch (WebException)
            {
                return false;
            }
        }

        private class CredentialParser : WebParser<bool>
        {
            public bool Parse(string html)
            {
                return true;
            }
        }
    }
}
