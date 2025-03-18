using System;
using System.Collections.Generic;
using Fantazee.Instance;
using Fantazee.Relics;
using Fantazee.Relics.Data;
using Fantazee.Relics.Instance;
using Fantazee.Scores;
using Fantazee.Scores.Instance;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Spells;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Shop.Ui.Screens
{
    public class ShopMainScreen : ShopScreen
    {
        private Action<ShopSpellButton> onSpellSelected;
        private Action<RelicEntry> onRelicSelected;
        private Action<ShopScoreButton> onScoreSelected;

        [Header("Prefabs")]

        [SerializeField]
        private ShopSpellButton shopSpellButtonPrefab;
        
        [SerializeReference]
        private List<ShopSpellButton> spellEntries = new();
        
        [SerializeField]
        private RelicEntry relicEntryPrefab;

        [SerializeReference]
        private List<RelicEntry> relicEntries = new();

        [SerializeField]
        private ShopScoreButton shopScoreButtonPrefab;
        
        [SerializeReference]
        private List<ShopScoreButton> scorePurchaseEntries = new();
        
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
                               Action<RelicEntry> onRelicSelected,
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
                RelicEntry relicEntry = Instantiate(relicEntryPrefab, relicContent);
                relicEntry.Initialize(r, OnRelicSelected);
                
                relicEntries.Add(relicEntry);
            }
        }

        private void OnSpellSelected(ShopSpellButton spellButton)
        {
            Debug.Log("onSpellSelected");
            onSpellSelected?.Invoke(spellButton);
        }

        private void OnRelicSelected(RelicEntry relicEntry)
        {
            Debug.Log($"Shop: OnRelicSelected: {relicEntry}");
            onRelicSelected?.Invoke(relicEntry);
        }

        private void OnScoreSelected(ShopScoreButton scoreButton)
        {
            onScoreSelected?.Invoke(scoreButton);
        }
    }
}
