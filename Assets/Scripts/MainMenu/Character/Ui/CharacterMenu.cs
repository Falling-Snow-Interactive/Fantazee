using Fantazee.Characters;
using Fantazee.Characters.Settings;
using Fantazee.Relics.Ui;
using Fantazee.Scores;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.Buttons;
using Fantazee.Ui.Buttons;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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
        private List<ScoreButton> scoreEntries;

        [SerializeField]
        private RelicEntryUi relic;

        [SerializeField]
        private Button confirmButton;

        [SerializeField]
        private SimpleButton leftButton;
        
        [SerializeField]
        private SimpleButton rightButton;

        [Header("     Input")]

        [SerializeField]
        private InputActionReference moveActionRef;
        private InputAction moveAction;
        
        private void Awake()
        {
            moveAction = moveActionRef.ToInputAction();
        }

        private void OnEnable()
        {
            moveAction.performed += OnNavigate;
        }

        private void OnDisable()
        {
            moveAction.performed -= OnNavigate;
        }

        private void Start()
        {
            characters = new List<CharacterData>(CharacterSettings.Settings.Characters);
            ShowCharacter(0);
        }

        public void Activate()
        {
            EventSystem.current.SetSelectedGameObject(confirmButton.gameObject);
        }
        
        #region Ui Buttons
        
        private void OnNavigate(InputAction.CallbackContext callbackContext)
        {
            float dir = moveAction.ReadValue<Vector2>().x;
            switch (dir)
            {
                case < 0:
                    leftButton.ClickFlash();
                    PrevCharacter();
                    break;
                case > 0:
                    rightButton.ClickFlash();
                    NextCharacter();
                    break;
            }
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
            nameText.text = character.Name;
            descText.text = character.Description;

            for (int j = 0; j < scoreEntries.Count; j++)
            {
                ScoreButton scoreButton = scoreEntries[j];
                ScoreInstance score = ScoreFactory.CreateInstance(character.Scores[j]);
                scoreButton.Initialize(score, null);
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
