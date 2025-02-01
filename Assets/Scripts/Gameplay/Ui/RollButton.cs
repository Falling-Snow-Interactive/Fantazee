using UnityEngine;

namespace ProjectYahtzee.Gameplay.Ui
{
    public class RollButton : MonoBehaviour
    {
        public void OnClick()
        {
            GameplayController.Instance.TryRoll();
        }
    }
}