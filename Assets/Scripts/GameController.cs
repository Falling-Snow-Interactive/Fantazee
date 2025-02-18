using Fsi.Gameplay;
using ProjectYahtzee.Boons;
using ProjectYahtzee.Instance;
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
            gameInstance = GameInstance.Defaults;
        }
    }
}
