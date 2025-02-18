using UnityEngine;

namespace ProjectYahtzee
{
    public class Launch : MonoBehaviour
    {
        private void Start()
        {
            ProjectSceneManager.Instance.LoadMainMenu();
        }
    }
}
