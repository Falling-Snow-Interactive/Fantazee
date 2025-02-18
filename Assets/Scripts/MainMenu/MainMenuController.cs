using UnityEngine;

namespace ProjectYahtzee.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        public void OnContinueButton()
        {
            Debug.Log("MainMenu - OnContinueButton");
            ProjectSceneManager.Instance.LoadMap();
        }

        public void OnNewGameButton()
        {
            Debug.Log("MainMenu - OnNewGameButton");
            GameController.Instance.NewGame();
            ProjectSceneManager.Instance.LoadMap();
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
