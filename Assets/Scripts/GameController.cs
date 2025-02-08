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
        }
        
        #endregion

        [SerializeField]
        private GameInstance gameInstance;
        public GameInstance GameInstance => gameInstance;

        protected override void Awake()
        {
            base.Awake();
            
            gameInstance.Seed = (uint)Random.Range(0, int.MaxValue);
            
            // TODO - Temporary just reseting the dice everytime beause they were getting cleared ???
            gameInstance.ResetDice();
            
            // Just gonna add a boon
            gameInstance.Boons.Add(BoonFactory.Create(BoonType.TwoMod));
        }
    }
}
