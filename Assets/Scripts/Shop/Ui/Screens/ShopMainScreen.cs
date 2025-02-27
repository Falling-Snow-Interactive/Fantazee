
using System;
using System.Collections.Generic;
using Fantazee.Currencies.Ui;
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
        
        private void Awake()
        {
            transform.localPosition = localIn;
        }

        public void Initialize(List<SpellType> spells, Action<SpellEntry> onSpellSelected)
        {
            this.onSpellSelected = onSpellSelected;
            
            foreach (SpellType spell in spells)
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
