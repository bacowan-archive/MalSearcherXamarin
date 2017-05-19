using Database;
using Database.Objects;
using MalSearcher.Model.MalProxy.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using static MalSearcher.Model.MalProxy.XmlUtils;

namespace MalSearcher.Model.MalProxy.Parsers
{
    public class SearchResultParser : WebParser<ICollection<Anime>>
    {
        public ICollection<Anime> Parse(string xml)
        {
            var anime = new HashSet<Anime>();
            XElement rootElement = XElement.Parse(xml);
            IEnumerable<XElement> animeNodes = ParseMultipleNodes(rootElement, "entry");
            foreach (var xmlAnime in animeNodes)
                anime.Add(parseAnimeElement(xmlAnime));
            return anime;
        }

        private Anime parseAnimeElement(XElement animeElement)
        {
            Anime anime = new Anime();
            anime.AnimeID = ParseSingleNode<int>(animeElement, "id");
            anime.Title = ParseSingleNode<string>(animeElement, "title");
            anime.Synonyms = ParseSingleNode<string>(animeElement, "synonyms");
            anime.Episodes = ParseSingleNode<int>(animeElement, "episodes");
            anime.Type = SeriesTypeExtensions.StringToSeriesType(ParseSingleNode<string>(animeElement, "type"));
            anime.Status = SeriesStatusExtensions.StringToSeriesStatus(ParseSingleNode<string>(animeElement, "status"));
            anime.Start = Datetime(ParseSingleNode<string>(animeElement, "start_date"));
            anime.End = Datetime(ParseSingleNode<string>(animeElement, "end_date"));
            anime.Synopsis = ParseSingleNode<string>(animeElement, "synopsis");
            anime.ImageURL = ParseSingleNode<string>(animeElement, "image");
            return anime;
        }
    }
}
