using UnityEngine;

namespace Fantazhee
{
    public class Launch : MonoBehaviour
    {
        private void Start()
        {
            GameController.Instance.LoadMainMenu();
        }
    }
}
