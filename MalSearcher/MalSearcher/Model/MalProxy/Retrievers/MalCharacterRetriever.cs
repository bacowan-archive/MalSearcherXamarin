using MalSearcher.Model.MalProxy.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Objects;
using MalSearcher.Model.MalProxy.Parsers;
using Database.Objects.Intersections;

namespace MalSearcher.Model.MalProxy.Retrievers
{
    class MalCharacterRetriever : CharacterRetriever
    {
        private const string MAIN_URL = "https://myanimelist.net/anime/{0}";
        //private const string CHARACTER_URL = "https://myanimelist.net/anime/{0}/{1}/characters";

        private BaseWebRetriever<ICollection<Character>> mRetriever;
        private BaseWebRetriever<string> mLinkRetriever;
        //private CharacterConsolidator mConsolidator;
        private CharacterListLinkParser mLinkListParser;

        public MalCharacterRetriever()
        {
            mRetriever = new BaseWebRetriever<ICollection<Character>>(new CharacterListParser());
            mLinkListParser = new CharacterListLinkParser();
            mLinkRetriever = new BaseWebRetriever<string>(mLinkListParser);
            //mConsolidator = new CharacterConsolidator();
        }

        public ICollection<Character> Get(Anime anime)
        {
            mLinkListParser.ID = anime.AnimeID;
            string characterListLink = mLinkRetriever.Get(String.Format(MAIN_URL, anime.AnimeID));
            ICollection<Character> characters = mRetriever.Get(characterListLink);
            foreach (var character in characters)
            {
                character.CharacterAnimes.Add(new CharacterAnime() {
                    Character = character,
                    CharacterId = character.CharacterID,
                    Anime = anime,
                    AnimeId = anime.AnimeID
                });
            }
            //mConsolidator.Consolidate(anime, characters);
            return characters;
        }
    }
}
