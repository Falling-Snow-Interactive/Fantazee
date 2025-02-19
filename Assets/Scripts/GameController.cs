using Fsi.Gameplay;
using ProjectYahtzee.Battle;
using ProjectYahtzee.Boons;
using ProjectYahtzee.Instance;
using ProjectYahtzee.LoadingScreens;
using ProjectYahtzee.Maps;
using UnityEngine;

namespace ProjectYahtzee
{
    public class GameController : MbSingleton<GameController>
    {
        #region Launch
        
        private const string RESOURCE_PATH = "Game_CTRL";
    
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void GameLaunch()
        {
            Debug.Log("Starting Game Controller.");
        
            GameController prefab = Resources.Load<GameController>(RESOURCE_PATH);
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

        protected override void Awake()
        {
            base.Awake();
            gameInstance.RandomizeSeed();
        }

        public void NewGame()
        {
            gameInstance = GameInstance.Defaults;
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
                                   ProjectSceneManager.Instance.LoadBattle(null);
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
