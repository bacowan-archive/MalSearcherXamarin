using System;
using System.Collections.Generic;
using System.Text;
using Database.Objects;
using Database.Objects.Intersections;
using MalSearcher.Model.MalProxy;
using System.Linq;

namespace MalSearcher.ViewModel
{
    public class CrossReferencePageViewModel : BaseViewModel
    {
        private Character mSelectedCharacter;
        private Language mSelectedLanguage;
        private DatabaseManager mDbManager;

        public CrossReferencePageViewModel(DatabaseManager dbManager, Character selectedCharacter, Language selectedLanguage)
        {
            mSelectedCharacter = selectedCharacter;
            mSelectedLanguage = selectedLanguage;
            mDbManager = dbManager;
            OnPropertyChanged("Roles");
        }

        public List<CrossReferenceList> Roles
        {
            get
            {
                IEnumerable<CharacterActor> characterActors = mDbManager.CrossReference(mSelectedCharacter, mSelectedLanguage);
                var groupedCharacters = characterActors.GroupBy(ca => ca.Actor, ca => ca.Character);
                return groupedCharacters.Select(g => new CrossReferenceList(g)).ToList();
            }
        }

        public class CrossReferenceList : List<CharacterAnime>
        {
            public String VoiceActorName { get; private set; }
            
            public CrossReferenceList(IGrouping<VoiceActor,Character> characters) : base(characters.SelectMany(c => c.CharacterAnimes).Distinct(CharacterAnimeHashSet.Comparer))
            {
                VoiceActorName = characters.Key.Name.English;
            }
        }
    }
}
