using Fsi.Gameplay.Healths.Ui;
using UnityEngine;

namespace ProjectYahtzee.Gameplay.Player
{
    public class GameplayPlayer : MonoBehaviour
    {
        [Header("Health")]

        [SerializeField]
        private HealthUi healthUi;

        private void Start()
        {
            healthUi.Initialize(GameController.Instance.GameInstance.Health);
        }
    }
}