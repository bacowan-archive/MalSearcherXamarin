using Database;
using Database.Objects;
using MalSearcher.Model.MalProxy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using static MalSearcher.Model.MalProxy.XmlUtils;

namespace MalSearcher.Model.MalProxy.Parsers
{
    public class UserAnimeListParser : WebParser<User>
    {

        public User Parse(string userList)
        {
            User user = new User();
            XElement rootElement = XElement.Parse(userList);
            setUserInfo(user, rootElement);
            setAnimeList(user, rootElement);
            return user;
        }

        private void setUserInfo(User user, XElement rootElement)
        {
            XElement myinfo = ParseSingleNode(rootElement, "myinfo");

            user.UserId = ParseSingleNode<int>(myinfo, "user_id");
            user.Username = ParseSingleNode<string>(myinfo, "user_name");
            user.Watching = ParseSingleNode<int>(myinfo, "user_watching");
            user.Completed = ParseSingleNode<int>(myinfo, "user_completed");
            user.OnHold = ParseSingleNode<int>(myinfo, "user_onhold");
            user.Dropped = ParseSingleNode<int>(myinfo, "user_dropped");
            user.PlanToWatch = ParseSingleNode<int>(myinfo, "user_plantowatch");
            user.DaysSpentWatching = ParseSingleNode<double>(myinfo, "user_days_spent_watching");
        }

        private void setAnimeList(User user, XElement rootElement)
        {
            var list = new HashSet<MyAnimeEntry>();
            IEnumerable<XElement> animeList = ParseMultipleNodes(rootElement, "anime");
            foreach (var anime in animeList)
            {
                MyAnimeEntry entry = parseAnimeElement(anime);
                entry.User = user;
                entry.UserId = user.UserId;
                list.Add(entry);
            }
            user.AnimeList = list;
        }

        private MyAnimeEntry parseAnimeElement(XElement animeElement)
        {
            MyAnimeEntry entry = new MyAnimeEntry();
            entry.MyIdValue = ParseSingleNode<int>(animeElement, "my_id");
            entry.WatchedEpisodes = ParseSingleNode<int>(animeElement, "my_watched_episodes");
            entry.StartDate = Datetime(ParseSingleNode<string>(animeElement, "my_start_date"));
            entry.EndDate = Datetime(ParseSingleNode<string>(animeElement, "my_finish_date"));
            entry.Score = ParseSingleNode<int>(animeElement, "my_score");
            entry.Status = WatchStatusExtensions.IntToWatchStatus(ParseSingleNode<int>(animeElement, "my_status"));
            entry.RewatchValue = RewatchValueExtensions.IntToRewatchValue(ParseSingleNode<int>(animeElement, "my_rewatching"));
            entry.RewatchedEpisodes = ParseSingleNode<int>(animeElement, "my_rewatching_ep");
            entry.LastUpdated = ParseSingleNode<int>(animeElement, "my_last_updated");
            entry.Anime = parseSeriesAnimeElement(animeElement);
            entry.AnimeId = entry.Anime.AnimeID;
            return entry;
        }

        private Anime parseSeriesAnimeElement(XElement animeElement)
        {
            Anime anime = new Anime();
            anime.AnimeID = ParseSingleNode<int>(animeElement, "series_animedb_id");
            anime.Title = ParseSingleNode<string>(animeElement, "series_title");
            anime.Synonyms = ParseSingleNode<string>(animeElement, "series_synonyms");
            anime.Type = SeriesTypeExtensions.IntToSeriesType(ParseSingleNode<int>(animeElement, "series_type"));
            anime.Episodes = ParseSingleNode<int>(animeElement, "series_episodes");
            anime.Status = SeriesStatusExtensions.IntToSeriesStatus(ParseSingleNode<int>(animeElement, "series_status"));
            anime.Start = Datetime(ParseSingleNode<string>(animeElement, "series_start"));
            anime.End = Datetime(ParseSingleNode<string>(animeElement, "series_end"));
            anime.ImageURL = ParseSingleNode<string>(animeElement, "series_image");
            return anime;
        }

        

        
    }
}
