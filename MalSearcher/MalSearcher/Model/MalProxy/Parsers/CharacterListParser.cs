using Database;
using Database.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using HtmlAgilityPack;
using System.Linq;
using System.Text.RegularExpressions;
using MalSearcher.Model.MalProxy.Interfaces;
using Database.Objects.Intersections;

namespace MalSearcher.Model.MalProxy.Parsers
{
    class CharacterListParser : WebParser<ICollection<Character>>
    {
        private static Regex mLinkIdRegex = new Regex("[0-9]+");

        public ICollection<Character> Parse(string animeHtml)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(animeHtml);

            var characterNodes = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/table/tr/td[2]/div[1]/table[count(preceding-sibling::h2)=1]")?.ToArray() ?? new HtmlNode[0];

            HashSet<Character> characters = new HashSet<Character>();
            for (int i = 0; i < characterNodes.Count(); i++)
            {
                var characterTd = characterNodes[i].SelectSingleNode("./tr/td[2]");
                Character character = new Character();
                parseCharacter(character, characterTd);

                var actorsRows = characterNodes[i].SelectNodes("./tr/td[3]/table/tr")?.ToArray() ?? new HtmlNode[0];
                foreach (var actorRow in actorsRows)
                {
                    CharacterActor characterActor = actorFromRow(actorRow);
                    characterActor.Character = character;
                    characterActor.CharacterId = character.CharacterID;
                    character.Actors.Add(characterActor);
                }
                characters.Add(character);
            }

            return characters;
        }

        private void parseCharacter(Character character, HtmlNode node)
        {
            character.Name = new Name();
            HtmlNode nameNode = node.SelectSingleNode("./a");
            character.Name.English = commaDelimitedName(nameNode.InnerHtml);
            character.CharacterID = parseLinkId(nameNode.GetAttributeValue("href", ""));
        }

        private int parseLinkId(string link)
        {
            Match match = mLinkIdRegex.Match(link);
            if (match.Success)
                return Int32.Parse(match.Value);
            else
                return 0;
        }

        private CharacterActor actorFromRow(HtmlNode node)
        {
            var actorElement = node.SelectSingleNode("./td/a");
            CharacterActor ca = new CharacterActor();
            VoiceActor actor = new VoiceActor();
            actor.Name = new Name() { English = commaDelimitedName(actorElement.InnerHtml) };
            actor.VoiceActorID = parseLinkId(actorElement.GetAttributeValue("href", ""));
            ca.Actor = actor;
            ca.ActorId = actor.VoiceActorID;
            ca.Language = new Language(node.SelectSingleNode("./td/small").InnerHtml);
            return ca;
        }

        private string commaDelimitedName(string name)
        {
            string[] names = name.Split(',');
            if (names.Count() != 2)
                return name;
            return names[1].Trim() + " " + names[0].Trim();
        }
    }
}
