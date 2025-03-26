using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fantazee.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(button.gameObject);
        }

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