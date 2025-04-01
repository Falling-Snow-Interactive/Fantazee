using System;
using Fantazee.Instance;
using Fantazee.Relics;
using Fantazee.Relics.Data;
using Fantazee.Relics.Instance;
using Fantazee.Scores;
using Fantazee.Scores.Instance;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Spells;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.Screens
{
    public class ShopMainScreen : ShopScreen
    {
        private Action<ShopSpellButton> onSpellSelected;
        private Action<RelicShopEntry> onRelicSelected;
        private Action<ShopScoreButton> onScoreSelected;

        [Header("Prefabs")]

        [SerializeField]
        private ShopSpellButton shopSpellButtonPrefab;
        
        private readonly List<ShopSpellButton> spellEntries = new();
        public List<ShopSpellButton> SpellEntries => spellEntries;
        
        [FormerlySerializedAs("relicEntryPrefab")]
        [SerializeField]
        private RelicShopEntry relicShopEntryPrefab;

        private readonly List<RelicShopEntry> relicEntries = new();
        public List<RelicShopEntry> RelicEntries => relicEntries;

        [SerializeField]
        private ShopScoreButton shopScoreButtonPrefab;
        
        private readonly List<ShopScoreButton> scorePurchaseEntries = new();
        public List<ShopScoreButton> ScorePurchaseEntries => scorePurchaseEntries;
        
        [Header("References")]

        [FormerlySerializedAs("boonContent")]
        [SerializeField]
        private Transform spellContent;
        
        [SerializeField]
        private Transform relicContent;

        [SerializeField]
        private Transform scoreContent;
        
        private void Awake()
        {
            transform.localPosition = showPos;
        }

        public void Initialize(ShopInventory shopInventory, 
                               Action<ShopSpellButton> onSpellSelected,
                               Action<RelicShopEntry> onRelicSelected,
                               Action<ShopScoreButton> onScoreSelected)
        {
            this.onSpellSelected = onSpellSelected;
            this.onRelicSelected = onRelicSelected;
            this.onScoreSelected = onScoreSelected;
            
            foreach (SpellData spell in shopInventory.Spells)
            {
                SpellInstance s = SpellFactory.CreateInstance(spell);
                ShopSpellButton spellButton = Instantiate(shopSpellButtonPrefab, spellContent);
                spellButton.Initialize(s, OnSpellSelected);

                spellEntries.Add(spellButton);
            }

            foreach (ScoreShopItem sd in shopInventory.Scores)
            {
                ScoreInstance score = ScoreFactory.CreateInstance(sd.Data, sd.Spells);
                ShopScoreButton scorePurchase = Instantiate(shopScoreButtonPrefab, scoreContent);
                scorePurchase.Initialize(score, OnScoreSelected);
                scorePurchaseEntries.Add(scorePurchase);
            }

            foreach (RelicData relic in shopInventory.Relics)
            {
                RelicInstance r = RelicFactory.Create(relic, GameInstance.Current.Character);
                RelicShopEntry relicShopEntry = Instantiate(relicShopEntryPrefab, relicContent);
                relicShopEntry.Initialize(r, OnRelicSelected);
                
                relicEntries.Add(relicShopEntry);
            }

            if (relicEntries.Count > 0
                && spellEntries.Count > 0)
            {
                // spellEntries[0].Button.navigation
                //     = spellEntries[0].Button.navigation with
                //       {
                //           selectOnDown = relicEntries[0].Button,
                //       };
                //
                // relicEntries[0].Button.navigation
                //     = relicEntries[0].Button.navigation with
                //       {
                //           selectOnUp = spellEntries[0].Button,
                //       };
            }
            
            SelectFirstButton();
        }

        public void SelectFirstButton()
        {
            if (spellEntries.Count > 0)
            {
                EventSystem.current.SetSelectedGameObject(spellEntries[0].gameObject);
            }
            else if (scorePurchaseEntries.Count > 0)
            {
                EventSystem.current.SetSelectedGameObject(scorePurchaseEntries[0].gameObject);
            }
            else if (relicEntries.Count > 0)
            {
                EventSystem.current.SetSelectedGameObject(relicEntries[0].gameObject);
            }
        }

        private void OnSpellSelected(ShopSpellButton spellButton)
        {
            Debug.Log("onSpellSelected");
            onSpellSelected?.Invoke(spellButton);
        }

        private void OnRelicSelected(RelicShopEntry relicShopEntry)
        {
            Debug.Log($"Shop: OnRelicSelected: {relicShopEntry}");
            onRelicSelected?.Invoke(relicShopEntry);
        }

        private void OnScoreSelected(ShopScoreButton scoreButton)
        {
            onScoreSelected?.Invoke(scoreButton);
        }

        public override void Show(bool force = false, Action onComplete = null)
        {
            base.Show(force, () =>
                             {
                                 SelectFirstButton();
                                 onComplete?.Invoke();
                             });
        }
    }
}
