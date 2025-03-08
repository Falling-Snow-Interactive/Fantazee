using System.Collections.Generic;
using Fantazee.Characters;
using Fantazee.Characters.Settings;
using Fantazee.Relics.Ui;
using Fantazee.Scores;
using Fantazee.Scores.Instance;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.MainMenu.Character.Ui
{
    public class CharacterMenu : MonoBehaviour
    {
        [SerializeReference]
        private List<CharacterData> characters;
        private int characterIndex = 0;
        
        [Header("References")]
        
        [SerializeField]
        private Image characterSprite;
        
        [SerializeField]
        private TMP_Text nameText;

        [SerializeField]
        private TMP_Text descText;

        [SerializeField]
        private List<CharacterScoreEntry> scoreEntries;

        [SerializeField]
        private RelicEntryUi relic;

        private void Start()
        {
            characters = new List<CharacterData>(CharacterSettings.Settings.Characters);
            ShowCharacter(0);
        }
        
        #region Ui Buttons
        
        public void LeftButton()
        {
            PrevCharacter();
        }

        public void RightButton()
        {
            NextCharacter();
        }

        public void ConfirmButton()
        {
            SelectCharacter();
        }
        
        #endregion

        #region Characters

        private void SelectCharacter()
        {
            CharacterData character = characters[characterIndex];
            MainMenuController.Instance.NewGame(character);
        }

        private void ShowCharacter(int i)
        {
            CharacterData character = characters[i];
            characterSprite.sprite = character.Icon;
            nameText.text = character.LocName.GetLocalizedString();
            descText.text = character.LocDesc.GetLocalizedString();

            for (int j = 0; j < scoreEntries.Count; j++)
            {
                CharacterScoreEntry scoreEntry = scoreEntries[j];
                ScoreInstance score = ScoreFactory.CreateInstance(character.Scores[j]);
                scoreEntry.Initialize(score, null);
            }

            relic.ShowData(character.Relic);
        }

        private void PrevCharacter()
        {
            characterIndex--;
            if (characterIndex < 0)
            {
                characterIndex = characters.Count - 1;
            }
            
            ShowCharacter(characterIndex);
        }

        private void NextCharacter()
        {
            characterIndex++;
            characterIndex %= characters.Count;
            
            ShowCharacter(characterIndex);
        }

        #endregion
    }
}
