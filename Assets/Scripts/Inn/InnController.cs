using System.Collections;
using System.Reflection;
using Fantazee.Currencies;
using Fantazee.Currencies.Ui;
using Fantazee.Instance;
using FMODUnity;
using Fsi.Gameplay;
using Fsi.Gameplay.Healths.Ui;
using TMPro;
using UnityEngine;

namespace Fantazee.Inn
{
    public class InnController : MbSingleton<InnController>
    {
        [Header("Costs")]

        [SerializeField]
        private Currency mealCost;

        [SerializeField]
        private Currency roomCost;

        [Header("Healing")]

        [Tooltip("Healing is a modifier of max hp.")]
        [Range(0, 1)]
        [SerializeField]
        private float mealHealing;

        [Tooltip("Healing is a modifier of max hp.")]
        [Range(0, 1)]
        [SerializeField]
        private float roomHealing;

        [Header("Sfx")]

        [SerializeField]
        private EventReference purchaseSfx;

        [Header("References")]

        [SerializeField]
        private CurrencyEntryUi mealCostEntry;

        [SerializeField]
        private TMP_Text mealHealingText;
        
        [SerializeField]
        private CurrencyEntryUi roomCostEntry;
        
        [SerializeField]
        private TMP_Text roomHealingText;

        [SerializeField]
        private HealthUi healthUi;

        [SerializeField]
        private CurrencyEntryUi wallet;

        private bool purcahased;

        private void Start()
        {
            mealCostEntry.SetCurrency(mealCost);
            roomCostEntry.SetCurrency(roomCost);

            int health = GameInstance.Current.Character.Health.max;
            mealHealingText.text = $"+{Mathf.RoundToInt(health * mealHealing)}";
            roomHealingText.text = $"+{Mathf.RoundToInt(health * roomHealing)}";
            
            healthUi.Initialize(GameInstance.Current.Character.Health);

            if (GameInstance.Current.Character.Wallet.TryGetCurrency(CurrencyType.Gold, out Currency gold))
            {
                wallet.SetCurrency(gold);
            }
            
            purcahased = false;
            
            GameController.Instance.InnReady();
        }
        
        public void OnSelectMeal()
        {
            if (purcahased)
            {
                return;
            }
            
            if (GameInstance.Current.Character.Wallet.CanAfford(mealCost))
            {
                purcahased = true;
                GameInstance.Current.Character.Health.Heal(Mathf.RoundToInt(GameInstance.Current.Character.Health.max * mealHealing));
                RuntimeManager.PlayOneShot(purchaseSfx);
                StartCoroutine(DelayedLeave());
            }
            else
            {
                mealCostEntry.PlayCantAfford();
            }
        }

        public void OnSelectRoom()
        {
            if (purcahased)
            {
                return;
            }
            
            if (GameInstance.Current.Character.Wallet.CanAfford(roomCost))
            {
                purcahased = true;
                GameInstance.Current.Character.Health.Heal(Mathf.RoundToInt(GameInstance.Current.Character.Health.max * roomHealing));
                RuntimeManager.PlayOneShot(purchaseSfx);
                StartCoroutine(DelayedLeave());
            }
            else
            {
                roomCostEntry.PlayCantAfford();
            }
        }

        public void LeaveButton()
        {
            if (!purcahased)
            {
                GameController.Instance.LoadMap();
            }
        }

        private IEnumerator DelayedLeave()
        {
            yield return new WaitForSeconds(1f);
            GameController.Instance.LoadMap();
        }
    }
}