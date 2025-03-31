using UnityEngine.UI;

namespace Fantazee.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button button;

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