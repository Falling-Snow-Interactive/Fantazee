using Fantahzee.Blacksmith.Ui;
using UnityEngine;

namespace Fantahzee.Blacksmith
{
    public class BlacksmithController : MonoBehaviour
    {
        [SerializeField]
        private BlacksmithUi blacksmithUi;

        private void Start()
        {
            Debug.Log("Blacksmith - Start");
            
            blacksmithUi.gameObject.SetActive(true);
            blacksmithUi.Initialize();
            
            GameController.Instance.BlacksmithReady();
        }
        
        public void ExitBlacksmith()
        {
            Debug.Log("Blacksmith - Exit");
            GameController.Instance.ExitBlacksmith();
        }
    }
}