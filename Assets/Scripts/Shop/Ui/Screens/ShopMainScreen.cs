using System;
using System.Collections.Generic;
using Fantazee.Instance;
using Fantazee.Relics;
using Fantazee.Relics.Instance;
using Fantazee.Scores;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Shop.Items;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using Fantazee.Spells.Settings;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Shop.Ui.Screens
{
    public class ShopMainScreen : ShopScreen
    {
        private Action<SpellEntry> onSpellSelected;
        private Action<RelicEntry> onRelicSelected;
        private Action<ShopScoreEntryPurchase> onScoreSelected;
        
        [Header("Prefabs")]
        
        [SerializeField]
        private SpellEntry spellEntryPrefab;
        
        [SerializeReference]
        private List<SpellEntry> spellEntries = new();
        
        [SerializeField]
        private RelicEntry relicEntryPrefab;

        [SerializeReference]
        private List<RelicEntry> relicEntries = new();

        [SerializeField]
        private ShopScoreEntryPurchase scorePurchaseEntry;
        
        [SerializeReference]
        private List<ShopScoreEntryPurchase> scorePurchaseEntries = new();
        
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
                               Action<SpellEntry> onSpellSelected,
                               Action<RelicEntry> onRelicSelected,
                               Action<ShopScoreEntryPurchase> onScoreSelected)
        {
            this.onSpellSelected = onSpellSelected;
            this.onRelicSelected = onRelicSelected;
            this.onScoreSelected = onScoreSelected;
            
            foreach (SpellShopItem spell in shopInventory.Spells)
            {
                SpellInstance s = SpellFactory.CreateInstance(spell.Item);
                SpellEntry spellEntry = Instantiate(spellEntryPrefab, spellContent);
                spellEntry.Initialize(s, OnSpellSelected);

                spellEntries.Add(spellEntry);
            }

            foreach (ScoreShopItem sd in shopInventory.Scores)
            {
                ScoreInstance score = ScoreFactory.CreateInstance(sd.Item);
                ShopScoreEntryPurchase scorePurchase = Instantiate(scorePurchaseEntry, scoreContent);
                scorePurchase.Initialize(score, OnScoreSelected);
                scorePurchaseEntries.Add(scorePurchase);
            }

            foreach (RelicShopItem relic in shopInventory.Relics)
            {
                RelicInstance r = RelicFactory.Create(relic.Item, GameInstance.Current.Character);
                RelicEntry relicEntry = Instantiate(relicEntryPrefab, relicContent);
                relicEntry.Initialize(r, OnRelicSelected);
                
                relicEntries.Add(relicEntry);
            }
        }

        private void OnSpellSelected(SpellEntry spellEntry)
        {
            onSpellSelected?.Invoke(spellEntry);
        }

        private void OnRelicSelected(RelicEntry relicEntry)
        {
            Debug.Log($"Shop: OnRelicSelected: {relicEntry}");
            onRelicSelected?.Invoke(relicEntry);
        }

        private void OnScoreSelected(ScoreEntry shopScoreEntry)
        {
            Debug.Assert(shopScoreEntry is ShopScoreEntryPurchase, 
                         "Should be a purchase score entry.", 
                         gameObject);
            
            onScoreSelected?.Invoke((ShopScoreEntryPurchase)shopScoreEntry);
        }
    }
}
