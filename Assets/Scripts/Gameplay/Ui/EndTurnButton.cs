using UnityEngine;

namespace ProjectYahtzee.Gameplay.Ui
{
    public class EndTurnButton : MonoBehaviour
    {
        public void OnClick()
        {
            GameplayController.Instance.TryEndTurn();
        }
    }
}