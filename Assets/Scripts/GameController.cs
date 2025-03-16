using System;
using Fantazee.Audio;
using Fantazee.Battle;
using Fantazee.Characters;
using Fantazee.Environments;
using Fantazee.Instance;
using Fantazee.LoadingScreens;
using Fantazee.Maps;
using Fsi.Gameplay;
using UnityEngine;
using UnityEngine.Serialization;

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
        
        [FormerlySerializedAs("environmentAudio")]
        [Header("Environment")]
            
        [SerializeField]
        private MusicController musicController;

        private FsiInput input;
        
        protected override void Awake()
        {
            base.Awake();
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

        public void NewGame(CharacterData character, EnvironmentData environment)
        {
            gameInstance?.Clear();
            gameInstance = new GameInstance(character, environment);
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
                                   ProjectSceneManager.Instance.LoadMap(GameInstance.Environment.Data, null);
                               });
        }

        public void MapReady()
        {
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
                                   GameInstance.Environment.Advance();
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
                                   ProjectSceneManager.Instance.LoadBattle(GameInstance.Environment.Data);
                               });
        }

        public void LoadBossBattle()
        {
            loadingScreen.Show(0,
                               () =>
                               {
                                   ProjectSceneManager.Instance.LoadBossBattle(GameInstance.Environment.Data);
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
        
        #region Shop
        
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
        
        #region Inn
        
        public void LoadInn()
        {
            loadingScreen.Show(0,
                               () =>
                               {
                                   ProjectSceneManager.Instance.LoadInn(null);
                               });
        }

        public void InnReady()
        {
            loadingScreen.Hide(0,
                               () =>
                               {
                                   
                               });
        }

        public void ExitInn()
        {
            LoadMap();
        }
        
        #endregion

        #region Encounters

        public void LoadEncounter()
        {
            loadingScreen.Show(0,
                               () =>
                               {
                                   ProjectSceneManager.Instance.LoadEncounter(null);
                               });
        }

        public void EncounterReady()
        {
            loadingScreen.Hide(0,
                               () =>
                               {
                                   
                               });
        }

        #endregion
    }
}
