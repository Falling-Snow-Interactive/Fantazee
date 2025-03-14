using System;
using DG.Tweening;
using Fantazee.Battle.Score.Ui;
using Fantazee.Instance;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.Buttons;
using Fantazee.Shop.Settings;
using Fantazee.Spells;
using Fantazee.Spells.Ui;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Scores.Scoresheets.Ui
{
    public class ScoresheetUpgradeScreen : MonoBehaviour
    {
        private Action onComplete;
        
        [SerializeField]
        private ScoresheetUi scoresheet;
        
        [Header("To Receive")]

        [SerializeField]
        private SpellButton toReceiveSpell;
        
        [SerializeField]
        private ScoreButton toReceiveScore;

        [Header("To Upgrade")]

        [SerializeField]
        private Transform toUpgradeContainer;
        
        private ScoreButton toUpgradeScore;
        private SpellButton toUpgradeSpell;

        [Header("Upgrade Sequence")]

        [SerializeField]
        private Transform animGroup;

        [SerializeField]
        private Image fadeImage;
        
        [Header("Animation Properties")]
        
        [SerializeField]
        private float selectTime = 0.6f;
        
        [SerializeField]
        private Ease selectEase = Ease.InCubic;
        
        [Space(10)]
        
        [SerializeField]
        private float chargeTime = 0.7f;
        
        [SerializeField]
        private Ease chargeEase = Ease.InCubic;

        [SerializeField]
        private float chargeOffset = 200f;
        
        [Space(10)]

        [SerializeField]
        private float crashTime = 0.5f;
        
        [SerializeField]
        private Ease crashEase = Ease.InCubic;
        
        [Space(10)]

        [SerializeField]
        private float punchAmount = 250f;

        [SerializeField]
        private float punchTime = 0.25f;
        
        [Space(10)]

        [SerializeField]
        private float returnTime = 1f;
        
        [SerializeField]
        private Ease returnEase = Ease.InCubic;
        
        [Space(10)]

        [SerializeField]
        protected float fadeAmount = 0.6f;
        
        [SerializeField]
        protected float fadeTime = 0.5f;
        
        [SerializeField]
        protected Ease fadeEase = Ease.InOutCubic;
        
        [Header("Audio")]

        [SerializeField]
        private EventReference chargeSfx;

        [SerializeField]
        private EventReference swooshSfx;
        
        [SerializeField]
        private EventReference upgradeSfx;

        public void StartScoreUpgrade(ScoreInstance scoreToReceive, Action onComplete = null)
        {
            this.onComplete = onComplete;
            scoresheet.Initialize(GameInstance.Current.Character.Scoresheet);
            
            toReceiveSpell.gameObject.SetActive(false);
            toReceiveScore.gameObject.SetActive(true);
            toReceiveScore.Initialize(scoreToReceive, null);

            scoresheet.RequestScore(OnScoreSelect);
        }
        
        public void StartSpellUpgrade(SpellInstance spellToReceive, Action onComplete = null)
        {
            this.onComplete = onComplete;
            scoresheet.Initialize(GameInstance.Current.Character.Scoresheet);
            
            toReceiveSpell.gameObject.SetActive(true);
            toReceiveScore.gameObject.SetActive(false);
            toReceiveSpell.Initialize(spellToReceive);

            scoresheet.RequestSpell(OnSpellSelect);
        }

        private void OnScoreSelect(ScoreButton scoreToUpgrade)
        {
            toUpgradeScore = scoreToUpgrade;
            ScoreSelectSequence(toReceiveScore.transform, scoreToUpgrade, () => onComplete?.Invoke());
        }

        private void OnSpellSelect(ScoreButton scoreToUpgrade, SpellButton spellToUpgrade)
        {
            toUpgradeScore = scoreToUpgrade;
            toUpgradeSpell = spellToUpgrade;
            ScoreSelectSequence(toReceiveSpell.transform, scoreToUpgrade, () => onComplete?.Invoke());
        }
        
        private void Apply()
        {
            Debug.Log($"Scoresheet Upgrade: {toUpgradeScore.Score} {toUpgradeSpell.Spell} -> {toReceiveSpell.Spell}");
            int index = toUpgradeScore.Score.Spells.IndexOf(toUpgradeSpell.Spell);
            toUpgradeScore.Score.Spells[index] = toReceiveSpell.Spell;
        }
        
        #region Animation Sequence
        
        private void ScoreSelectSequence(Transform toReceiveTransform, 
                                           ScoreButton toUpgradeButton, 
                                           Action onComplete = null)
        {
            Vector3 pos = toReceiveScore.transform.position;
            
            Transform returnParent = toUpgradeButton.transform.parent;
            toUpgradeButton.transform.SetParent(animGroup, true);
            
            Sequence sequence = DOTween.Sequence();
            
            // Selected score to upgrade area
            Vector3 toReceivePos = toReceiveTransform.position;
            Vector3 toUpgradePos = toUpgradeContainer.position;
            Vector3 mid = (toReceivePos + toUpgradePos) / 2f;
            Vector3 toReceiveChargePos = toReceivePos + Vector3.left * chargeOffset;
            Vector3 toUpgradeChargePos = toUpgradePos + Vector3.right * chargeOffset;

            sequence.Append(toUpgradeButton.transform.DOMove(toUpgradePos, selectTime)
                                       .SetEase(selectEase)
                                       .OnStart(() => RuntimeManager.PlayOneShot(swooshSfx)));
            // Fade background
            sequence.Insert(0, fadeImage.DOFade(fadeAmount, fadeTime)
                                        .SetEase(fadeEase)
                                        .OnStart(() => fadeImage.raycastTarget = true));

            // // Charge up the scores
            float chargeInsertTime = selectTime + 0.25f;
            sequence.Insert(chargeInsertTime, toReceiveTransform.DOMove(toReceiveChargePos,
                                                                        chargeTime)
                                                                .SetEase(chargeEase)
                                                                .OnStart(() =>
                                                                         {
                                                                             RuntimeManager.PlayOneShot(ShopSettings
                                                                                 .Settings.ChargeSfx);
                                                                         }));
            sequence.Insert(chargeInsertTime, toUpgradeButton.transform.DOMove(toUpgradeChargePos,
                                                                               chargeTime)
                                                             .SetEase(chargeEase));

            sequence.Insert(chargeInsertTime, toReceiveTransform.DOShakeRotation(chargeTime,
                                                                         new Vector3(0, 0, 3),
                                                                         30,
                                                                         90f,
                                                                         false,
                                                                         ShakeRandomnessMode.Full)
                                                                .SetEase(Ease.OutQuad));
            sequence.Insert(chargeInsertTime, toReceiveTransform.DOShakeRotation(chargeTime,
                                                                    new Vector3(0, 0, 3),
                                                                    30,
                                                                    90f,
                                                                    false,
                                                                    ShakeRandomnessMode.Full)
                                                                .SetEase(Ease.OutQuad));
            
            // Move in to combine
            float crashInsertTime = chargeInsertTime + chargeTime;
            sequence.Insert(crashInsertTime, toReceiveTransform.DOMove(mid, crashTime)
                                                               .SetEase(crashEase)
                                                               .OnStart(() => RuntimeManager.PlayOneShot(swooshSfx))
                                                               .OnComplete(() =>
                                                                           {
                                                                               toReceiveTransform.gameObject.SetActive(false);
                                                                               Apply();
                                                                               toUpgradeButton.UpdateVisuals();
                                                                           }));
            sequence.Insert(crashInsertTime, toUpgradeButton.transform.DOMove(mid, crashTime)
                                                           .SetEase(crashEase));

            sequence.Append(toUpgradeButton.transform.DOPunchScale(Vector3.one * punchAmount, punchTime, 5, 1f));
            
            float returnInsertTime = crashTime + crashInsertTime + punchTime;
            sequence.Insert(returnInsertTime, toReceiveScore.transform.DOMove(pos, returnTime)
                                                            .OnStart(() => RuntimeManager.PlayOneShot(swooshSfx))
                                                            .SetEase(returnEase)
                                                            .SetDelay(0.3f));
            sequence.Insert(returnInsertTime, fadeImage.DOFade(0, fadeTime)
                                                       .SetEase(fadeEase)
                                                       .OnComplete(() => fadeImage.raycastTarget = false));
            
            sequence.AppendInterval(0.5f);

            sequence.OnComplete(() =>
                                {
                                    toReceiveScore.transform.SetParent(returnParent);
                                    onComplete?.Invoke();
                                });
            sequence.Play();
        }
        
        #endregion
    }
}