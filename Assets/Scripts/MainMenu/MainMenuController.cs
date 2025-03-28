using Fantazee.Audio;
using Fantazee.Characters;
using Fantazee.Environments;
using Fantazee.MainMenu.Character.Ui;
using Fsi.Gameplay;
using UnityEngine;

namespace Fantazee.MainMenu
{
    public class MainMenuController : MbSingleton<MainMenuController>
    {
        [SerializeField]
        private MainMenu mainMenu;
        
        [SerializeField]
        private CharacterMenu characterMenu;

        protected override void Awake()
        {
            base.Awake();
            characterMenu.gameObject.SetActive(false);
        }
        
        private void Start()
        {
            MusicController.Instance.PlayMusic(MusicId.Menu);
            GameController.Instance.MainMenuReady();
        }
        
        #region New Game

        public void NewGame()
        {
            NewGame(CharacterData.Default, EnvironmentData.Default);
        }

        public void NewGame(CharacterData character)
        {
            NewGame(character, EnvironmentData.Starting);
        }

        public void NewGame(EnvironmentData environment)
        {
            NewGame(CharacterData.Default, environment);
        }

        public void NewGame(CharacterData character, EnvironmentData environment)
        {
            Debug.Log($"MainMenu - Starting new game as {character.name}", character);
            GameController.Instance.NewGame(character, environment);
            GameController.Instance.LoadMap();
        }
        
        #endregion

        public void ShowMainMenu()
        {
            mainMenu.gameObject.SetActive(true);
            characterMenu.gameObject.SetActive(false);
        }

        public void ShowCharacterMenu()
        {
            characterMenu.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(false);
            
            characterMenu.Activate();
        }

        public void Quit()
        {
            Debug.Log("MainMenu - OnQuitButton");
            Application.Quit();
            
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
