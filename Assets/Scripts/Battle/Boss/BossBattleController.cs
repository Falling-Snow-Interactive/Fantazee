
namespace Fantazee.Battle.Boss
{
    public class BossBattleController : BattleController
    {
        protected override void BattleWin()
        {
            base.BattleWin();

            GameController.Instance.GameInstance.Map.SetReadyToAdvance();
        }
    }
}