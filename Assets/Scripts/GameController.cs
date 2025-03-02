using System;
using Fantazee.Battle;
using Fantazee.Battle.Environments;
using Fantazee.Characters;
using Fantazee.Environments.Audio;
using Fantazee.Instance;
using Fantazee.LoadingScreens;
using Fantazee.Maps;
using FMODUnity;
using Fsi.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fantazee
{
    public class GameController : MbSingleton<GameController>
    {
        #region Launch
        
        private const string ResourcePath = "Game_CTRL";
    
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void GameLaunch()
        {
            Debug.Log("Starting Game Controller.");
        
            GameController prefab = Resources.Load<GameController>(ResourcePath);
            Instantiate(prefab).name = "Game_CTRL";
            
            Application.targetFrameRate = 60;
        }
        
        #endregion

        [SerializeField]
        private GameInstance gameInstance;
        public GameInstance GameInstance => gameInstance;
        
        [Header("Loading")]
        
        [SerializeField]
        private LoadingScreen loadingScreen;
        
        [Header("Environment")]
            
        [SerializeField]
        private EnvironmentAudio environmentAudio;

        private FsiInput input;
        
        protected override void Awake()
        {
            base.Awake();
            gameInstance.RandomizeSeed();
            gameInstance = GameInstance.Defaults;
            
            input = new FsiInput();

            input.Gameplay.Exit.performed += ctx => Application.Quit();
        }

        private void OnEnable()
        {
            input.Gameplay.Enable();
        }

        private void OnDisable()
        {
            input.Gameplay.Disable();
        }

        public void NewGame()
        {
            gameInstance = GameInstance.Defaults;
        }

        public void NewGame(CharacterData character)
        {
            gameInstance = new GameInstance(character);
        }

        public void LoadGame()
        {
            // TODO - Load a saved game instance.
        }

        public void SaveGame()
        {
            // TODO - Save the current game.
        }

        public void Reset()
        {
            dontDestroyOnLoad = true;
            gameInstance = GameInstance.Defaults;
        }

        #region Map
        
        public void LoadMap()
        {
            loadingScreen.Show(0,
                               () =>
                               {
                                   ProjectSceneManager.Instance.LoadMap(null);
                               });
        }

        public void MapReady()
        {
            PlayEnvironmentMusic();
            loadingScreen.Hide(0,
                               () =>
                               {
                                   MapController.Instance.StartMap();
                               });
        }

        public void AdvanceMap(Action onComplete)
        {
            loadingScreen.Show(0.5f,
                               () =>
                               {
                                   GameInstance.Map.Advance();
                                   // TODO this is also where the new map will be generated and then sent to the map controller to display
                                   loadingScreen.Hide(0.5f,
                                                      () =>
                                                      {
                                                          onComplete?.Invoke();
                                                      });
                               });
        }
        
        #endregion
        
        #region Battle

        public void LoadBattle()
        {
            loadingScreen.Show(0,
                               () =>
                               {
                                   ProjectSceneManager.Instance.LoadBattle(GameInstance.Map.Environment);
                               });
        }

        public void LoadBossBattle()
        {
            loadingScreen.Show(0,
                               () =>
                               {
                                   ProjectSceneManager.Instance.LoadBossBattle(GameInstance.Map.Environment);
                               });
        }

        public void BattleReady()
        {
            loadingScreen.Hide(0,
                               () =>
                               {
                                   BattleController.Instance.BattleStart();
                               });
        }

        public void FinishedBattle(bool win)
        {
            LoadMap();
        }
        
        #endregion
        
        #region Inn
        
        public void LoadShop()
        {
            loadingScreen.Show(0,
                               () =>
                               {
                                   ProjectSceneManager.Instance.LoadShop(null);
                               });
        }

        public void ShopReady()
        {
            loadingScreen.Hide(0,
                               () =>
                               {
                                   // BattleController.Instance.ShopStart();
                               });
        }

        public void ExitShop()
        {
            LoadMap();
        }
        
        #endregion

        #region Blacksmith

        public void LoadBlacksmith()
        {
            loadingScreen.Show(0,
                               () =>
                               {
                                   ProjectSceneManager.Instance.LoadBlacksmith(null);
                               });
        }

        public void BlacksmithReady()
        {
            loadingScreen.Hide(0,
                               () =>
                               {
                                   // BattleController.Instance.BlacksmithStart();
                               });
        }
        
        public void ExitBlacksmith()
        {
            LoadMap();
        }
        
        #endregion

        #region Main Menu

        public void LoadMainMenu()
        {
            loadingScreen.Show(0,
                               () =>
                               {
                                   ProjectSceneManager.Instance.LoadMainMenu(null);
                               });
        }

        public void MainMenuReady()
        {
            loadingScreen.Hide(0,
                               () => { });
        }
        
        #endregion

        #region Environment

        private void PlayEnvironmentMusic()
        {
            environmentAudio.PlayMusic(GameInstance.Current.Map.Environment);
        }

        #endregion
    }
}
