using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_01_Pierce", fileName = "Spell_01_Pierce", order = 1)]
    public class PierceSpellData : SpellData
    {
        public override SpellType Type => SpellType.Pierce;
        
        [Header("Pierce")]

        [Range(0,2)]
        [SerializeField]
        private float firstEnemyMod = 0.75f;
        public float FirstEnemyMod => firstEnemyMod;

        [Range(0, 2)]
        [SerializeField]
        private float secondEnemyMod = 0.25f;
        public float SecondEnemyMod => secondEnemyMod;
        
        [Header("Effects")]
        
        [SerializeField]
        private GameObject pierceVfx;
        public GameObject PierceVfx => pierceVfx;
        
        [SerializeField]
        private EventReference pierceSfx;
        public EventReference PierceSfx => pierceSfx;

        [SerializeField]
        private Vector3 pierceVfxSpawnOffset;
        public Vector3 PierceVfxSpawnOffset => pierceVfxSpawnOffset;
        
        [SerializeField]
        private Vector3 pierceVfxHitOffset;
        public Vector3 PierceVfxHitOffset => pierceVfxHitOffset;

        [SerializeField]
        private Ease pierceEase = Ease.Linear;
        public Ease PierceEase => pierceEase;

        [SerializeField]
        private float pierceTime = 0.6f;
        public float PierceTime => pierceTime;

        [SerializeField]
        private float pierceDelay = 0.35f;
        public float PierceDelay => pierceDelay;
        
        [Header("Hit")]

        [SerializeField]
        private GameObject hitVfx;
        public GameObject HitVfx => hitVfx;

        [SerializeField]
        private EventReference hitSfx;
        public EventReference HitSfx => hitSfx;

        protected override Dictionary<string, string> GetDescArgs()
        {
            Dictionary<string, string> dict = base.GetDescArgs();
            dict.Add("FirstMod", $"{firstEnemyMod * 100f}");
            dict.Add("SecondMod", $"{secondEnemyMod * 100f}");
            return dict;
        }
    }
}
