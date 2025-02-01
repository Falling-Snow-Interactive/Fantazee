using Fsi.Gameplay;
using ProjectYahtzee.Instance;
using UnityEngine;

namespace ProjectYahtzee
{
    public class GameController : MbSingleton<GameController>
    {
        private const string RESOURCE_PATH = "Game_CTRL";
    
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void GameLaunch()
        {
            Debug.Log("Starting Game Controller.");
        
            GameController prefab = Resources.Load<GameController>(RESOURCE_PATH);
            Instantiate(prefab).name = "Game_CTRL";
        }

        [SerializeField]
        private GameInstance gameInstance;
        public GameInstance GameInstance => gameInstance;
    }
}
