
using System;
using System.Collections.Generic;
using Fantazee.Battle.Settings;
using Fantazee.Currencies.Ui;
using Fantazee.Scores;
using Fantazee.Scores.Information;
using Fantazee.Shop.Ui.Entries;
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
        private Action<ScoreShopEntry> onScoreSelected;
        
        [Header("Prefabs")]
        
        [SerializeField]
        private SpellEntry spellEntryPrefab;
        
        [SerializeReference]
        private List<SpellEntry> spellEntries = new();
        
        [SerializeField]
        private RelicEntry relicEntryPrefab;

        [SerializeReference]
        private List<RelicEntry> relicEntries = new();
        
        [FormerlySerializedAs("scoreEntryPrefab")]
        [SerializeField]
        private ScoreShopEntry scoreShopEntryPrefab;
        
        [SerializeReference]
        private List<ScoreShopEntry> scoreEntries = new();
        
        [Header("References")]

        [SerializeField]
        private Transform boonContent;
        
        [SerializeField]
        private Transform relicContent;

        [SerializeField]
        private Transform scoreContent;
        
        private void Awake()
        {
            transform.localPosition = localIn;
        }

        public void Initialize(ShopInventory shopInventory, 
                               Action<SpellEntry> onSpellSelected,
                               Action<RelicEntry> onRelicSelected,
                               Action<ScoreShopEntry> onScoreSelected)
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

            foreach (ScoreType score in shopInventory.Scores)
            {
                ScoreShopEntry scoreShopEntry = Instantiate(scoreShopEntryPrefab, scoreContent);
                scoreShopEntry.Initialize(score, OnScoreSelected);
                scoreEntries.Add(scoreShopEntry);
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

        private void OnScoreSelected(ScoreShopEntry scoreShopEntry)
        {
            onScoreSelected?.Invoke(scoreShopEntry);
        }
    }
}
