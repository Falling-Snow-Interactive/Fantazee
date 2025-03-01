
using System;
using System.Collections.Generic;
using Fantazee.Battle.Settings;
using Fantazee.Currencies.Ui;
using Fantazee.Scores;
using Fantazee.Scores.Information;
using Fantazee.Scores.Ui;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Shop.Ui.ScoreSelect;
using Fantazee.Spells;
using Fantazee.Spells.Data;
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

        [SerializeField]
        private Transform boonContent;
        
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
            
            foreach (SpellType spell in shopInventory.Spells)
            {
                if (SpellSettings.Settings.TryGetSpell(spell, out SpellData data))
                {
                    SpellEntry spellEntry = Instantiate(spellEntryPrefab, boonContent);
                    spellEntry.Initialize(data, OnSpellSelected);

                    spellEntries.Add(spellEntry);
                }
                else
                {
                    Debug.LogWarning($"Shop: No spell found for type {spell}");
                }
            }

            foreach (ScoreData sd in shopInventory.Scores)
            {
                Score score = ScoreFactory.Create(sd);
                ShopScoreEntryPurchase scorePurchase = Instantiate(scorePurchaseEntry, scoreContent);
                scorePurchase.Initialize(score, OnScoreSelected);
                scorePurchaseEntries.Add(scorePurchase);
            }
        }

        private void OnSpellSelected(SpellEntry spellEntry)
        {
            onSpellSelected?.Invoke(spellEntry);
        }

        private void OnRelicSelected(RelicEntry relicEntry)
        {
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
