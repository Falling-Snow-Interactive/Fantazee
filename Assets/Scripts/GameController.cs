using System.Collections.Generic;
using Fantazee.Battle;
using Fantazee.Instance;
using Fantazee.LoadingScreens;
using Fantazee.Maps;
using Fantazee.Relics;
using Fsi.Gameplay;
using UnityEngine;

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

        [Header("Debug")]
        
        [SerializeField]
        private List<RelicData> startingRelics;
        
        protected override void Awake()
        {
            base.Awake();
            gameInstance.RandomizeSeed();
            gameInstance = GameInstance.Defaults;
        }

        public void NewGame()
        {
            gameInstance = GameInstance.Defaults;
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
            loadingScreen.Show(() =>
                               {
                                   ProjectSceneManager.Instance.LoadMap(null);
                               });
        }

        public void MapReady()
        {
            loadingScreen.Hide(() =>
                               {
                                   MapController.Instance.StartMap();
                               });
        }
        
        #endregion
        
        #region Battle

        public void LoadBattle()
        {
            loadingScreen.Show(() =>
                               {
                                   ProjectSceneManager.Instance.LoadBattle(GameInstance.Environment);
                               });
        }

        public void LoadBossBattle()
        {
            loadingScreen.Show(() =>
                               {
                                   ProjectSceneManager.Instance.LoadBossBattle(GameInstance.Environment);
                               });
        }

        public void BattleReady()
        {
            loadingScreen.Hide(() =>
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
            loadingScreen.Show(() =>
                               {
                                   ProjectSceneManager.Instance.LoadShop(null);
                               });
        }

        public void ShopReady()
        {
            loadingScreen.Hide(() =>
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
            loadingScreen.Show(() =>
                               {
                                   ProjectSceneManager.Instance.LoadBlacksmith(null);
                               });
        }

        public void BlacksmithReady()
        {
            loadingScreen.Hide(() =>
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
            loadingScreen.Show(() =>
                               {
                                   ProjectSceneManager.Instance.LoadMainMenu(null);
                               });
        }

        public void MainMenuReady()
        {
            loadingScreen.Hide(() => { });
        }
        
        #endregion
    }
}
