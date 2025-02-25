using TMPro;
using UnityEngine;

namespace Fantazee.Battle.Shields.Ui
{
    public class ShieldUi : MonoBehaviour
    {
        private Shield shield;
        
        [SerializeField]
        private GameObject root;

        [SerializeField]
        private TMP_Text amountText;

        private void Awake()
        {
            root.SetActive(false);
        }

        private void OnEnable()
        {
            if (shield != null)
            {
                shield.Changed += OnChanged;
            }
        }

        private void OnDisable()
        {
            if (shield != null)
            {
                shield.Changed -= OnChanged;
            }
        }

        public void Initialize(Shield shield)
        {
            root.SetActive(false);
            this.shield = shield;

            shield.Changed += OnChanged;
            
            root.SetActive(shield.Current > 0);
        }

        private void OnChanged()
        {
            root.SetActive(shield.Current > 0);
            amountText.text = shield.Current.ToString();
        }
    }
}