using Fantazee.Instance;
using Fantazee.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button continueButton;

        private void Start()
        {
            continueButton.gameObject.SetActive(SaveManager.TryLoadGame(out GameSave _));
        }
        
        public void OnNewGameButton()
        {
            MainMenuController.Instance.ShowCharacterMenu();
        }
        
        public void OnContinueButton()
        {
            MainMenuController.Instance.LoadGame();
        }
        
        public void OnSettingsButton()
        {
        }

        public void OnQuitButton()
        {
            MainMenuController.Instance.Quit();
        }
    }
}