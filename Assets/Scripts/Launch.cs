using UnityEngine;

namespace Fantazee
{
    public class Launch : MonoBehaviour
    {
        private void Start()
        {
            GameController.Instance.LoadMainMenu();
        }
    }
}
