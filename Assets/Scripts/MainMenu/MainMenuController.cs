using UnityEngine;

namespace Fantahzee.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        private void Start()
        {
            GameController.Instance.MainMenuReady();
        }
        
        public void OnContinueButton()
        {
            Debug.Log("MainMenu - OnContinueButton");
            GameController.Instance.LoadMap();
        }

        public void OnNewGameButton()
        {
            Debug.Log("MainMenu - OnNewGameButton");
            GameController.Instance.NewGame();
            GameController.Instance.LoadMap();
        }

        public void OnSettingsButton()
        {
            Debug.Log("MainMenu - OnSettingsButton - Not Implemented");
        }

        public void OnQuitButton()
        {
            Debug.Log("MainMenu - OnQuitButton");
            Application.Quit();
            
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
