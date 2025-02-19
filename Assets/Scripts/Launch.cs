using UnityEngine;

namespace Fantahzee
{
    public class Launch : MonoBehaviour
    {
        private void Start()
        {
            GameController.Instance.LoadMainMenu();
        }
    }
}
