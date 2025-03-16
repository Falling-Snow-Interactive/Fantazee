using UnityEngine;

namespace Fantazee.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public void OnNewGameButton()
        {
            MainMenuController.Instance.ShowCharacterMenu();
        }
        
        public void OnContinueButton()
        {
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