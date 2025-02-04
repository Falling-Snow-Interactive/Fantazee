using UnityEngine;

namespace ProjectYahtzee.Battle.Ui
{
    public class EndTurnButton : MonoBehaviour
    {
        public void OnClick()
        {
            BattleController.Instance.TryEndTurn();
        }
    }
}