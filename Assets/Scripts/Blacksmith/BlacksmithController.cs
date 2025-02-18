using ProjectYahtzee.Blacksmith.Ui;
using UnityEngine;

namespace ProjectYahtzee.Blacksmith
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
        }
        
        public void ExitBlacksmith()
        {
            Debug.Log("Blacksmith - Exit");
            ProjectSceneManager.Instance.LoadMap();
        }
    }
}