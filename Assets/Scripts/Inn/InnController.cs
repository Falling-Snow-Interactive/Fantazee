using System.Collections;
using DG.Tweening;
using Fantazee.Audio;
using Fantazee.Currencies;
using Fantazee.Currencies.Ui;
using Fantazee.Instance;
using FMOD.Studio;
using FMODUnity;
using Fsi.Gameplay;
using Fsi.Gameplay.Healths.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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

        [SerializeField]
        private EventReference ambianceSfxRef;
        private EventInstance ambianceSfx;

        [SerializeField]
        private EventReference mealSfx;
        
        [SerializeField]
        private EventReference roomSfx;

        [Header("Npc Anim")]

        [SerializeField]
        private Image npc;

        [SerializeField]
        private Vector3 npcOffset;

        [SerializeField]
        private Vector3 npcScale;

        [SerializeField]
        private float npcTime;

        [SerializeField]
        private Ease npcMoveEase = Ease.Linear;
        
        [SerializeField]
        private Ease npcScaleEase = Ease.Linear;
        
        [SerializeField]
        private Ease npcFadeEase = Ease.Linear;

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

        protected override void Awake()
        {
            base.Awake();

            ambianceSfx = RuntimeManager.CreateInstance(ambianceSfxRef);
        }
        
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

            ambianceSfx.start();
            MusicController.Instance.PlayMusic(MusicId.Inn);

            Vector3 move = npc.transform.localPosition;
            npc.transform.localPosition += npcOffset;
            npc.transform.DOLocalMove(move, npcTime).SetEase(npcMoveEase).SetDelay(0.5f);
            
            // Vector3 scale = npc.transform.localScale;
            // npc.transform.localScale = npcScale;
            // npc.transform.DOScale(scale, npcTime).SetEase(npcScaleEase);

            Color color = npc.color;
            color.a = 0;
            npc.color = color;
            npc.DOFade(1, npcTime).SetEase(npcFadeEase).SetDelay(0.5f);
            
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
                RuntimeManager.PlayOneShot(mealSfx);
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
                RuntimeManager.PlayOneShot(roomSfx);
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