using HtmlAgilityPack;
using MalSearcher.Model.MalProxy.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MalSearcher.Model.MalProxy.Parsers
{
    public class CharacterListLinkParser : WebParser<string>
    {
        //private static Regex mLinkIdRegex = new Regex("[0-9]+");
        private const string REGEX_STRING = "https://myanimelist.net/anime/{0}/.*/characters";

        public int? ID { get; set; }

        public string Parse(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            Regex mLinkIdRegex = new Regex(String.Format(REGEX_STRING, ID.ToString()));
            Match match = mLinkIdRegex.Match(html);
            return match.Value;
            //var node = doc.DocumentNode.SelectSingleNode("//*[@id=\"content\"]/table/tbody/tr/td[2]/div[1]/table/tbody/tr[2]/td/h2[2]/div/a");
            //return node.GetAttributeValue("href", "");
        }
    }
}
