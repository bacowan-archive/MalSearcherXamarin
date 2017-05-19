using MalSearcher.Model.MalProxy.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MalSearcher.Model.MalProxy.Retrievers
{
    class BaseWebRetriever<OutType> : WebRetriever<string, OutType>
    {
        private WebGetter mWebGetter;
        private WebParser<OutType> mParser;

        public BaseWebRetriever(WebParser<OutType> parser)
        {
            mWebGetter = new WebGetter();
            mParser = parser;
        }

        public int? ID { get; internal set; }

        public OutType Get(string url)
        {
            string rawHtml = mWebGetter.Get(url);
            return mParser.Parse(rawHtml);
        }

        public OutType Get(string url, string username, string password)
        {
            string rawHtml = mWebGetter.Get(url, username, password);
            return mParser.Parse(rawHtml);
        }
    }
}
