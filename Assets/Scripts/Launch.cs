using UnityEngine;

namespace Fantazee
{
    public class Launch : MonoBehaviour
    {
        public void Continue()
        {
            GameController.Instance.LoadMainMenu();
        }
        
        private void Start()
        {
            GameController.Instance.LoadMainMenu();
        }
    }
}
