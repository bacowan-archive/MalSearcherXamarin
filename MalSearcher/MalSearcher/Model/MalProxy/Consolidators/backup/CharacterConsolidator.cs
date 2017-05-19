using Database.Objects;
using Database.Objects.Intersections;
using MalSearcher.Model.MalProxy.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MalSearcher.Model.MalProxy
{
    /*class CharacterConsolidator
    {
        public void Consolidate(Anime anime, ICollection<Character> characters)
        {
            foreach (var character in characters)
            {
                CharacterAnime ca = new CharacterAnime()
                {
                    Character = character,
                    Anime = anime
                };
                anime.CharacterAnimes.Add(ca);
                character.CharacterAnimes.Add(ca);
            }
        }

        public void Consolidate(Character databaseCharacter, Character newCharacter)
        {
            databaseCharacter.Actors = newCharacter.Actors;
            databaseCharacter.CharacterAnimes = newCharacter.CharacterAnimes;
            databaseCharacter.CharacterID = newCharacter.CharacterID;
            databaseCharacter.Name = newCharacter.Name;
        }
    }*/
}
