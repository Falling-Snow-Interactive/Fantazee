using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Score.Ui;
using Fantazee.Instance;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.Buttons;
using Fantazee.Shop.Settings;
using Fantazee.Shop.Ui;
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

        [SerializeField]
        private List<MonoBehaviour> offWhileAnimating;

        [Header("Animation")]

        [Header("Show/Hide")]

        [SerializeField]
        private Vector3 showHidePosition;
        
        [SerializeField]
        private float showTime = 1f;

        [SerializeField]
        private Ease showEase;
        
        [SerializeField]
        private float hideTime = 1f;
        
        [SerializeField]
        private Ease hideEase;
        
        [Header("Upgrade Sequence")]
        
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

        [Header("References")]

        [SerializeField]
        private Transform root;

        private bool isSpellUpgrade = false;

        public void StartScoreUpgrade(ScoreInstance scoreToReceive, Action onComplete = null)
        {
            isSpellUpgrade = false;
            
            this.onComplete = onComplete;
            scoresheet.Initialize(GameInstance.Current.Character.Scoresheet);
            
            toReceiveSpell.gameObject.SetActive(false);
            toReceiveScore.gameObject.SetActive(true);
            toReceiveScore.transform.localPosition = Vector3.zero;
            toReceiveScore.transform.localRotation = Quaternion.identity;
            toReceiveScore.transform.localScale = Vector3.one;
            toReceiveScore.Initialize(scoreToReceive, null);

            scoresheet.RequestScore(OnScoreSelect, true);
        }
        
        public void StartSpellUpgrade(SpellInstance spellToReceive, Action onComplete = null)
        {
            isSpellUpgrade = true;
            
            this.onComplete = onComplete;
            scoresheet.Initialize(GameInstance.Current.Character.Scoresheet);
            
            toReceiveSpell.gameObject.SetActive(true);
            toReceiveScore.gameObject.SetActive(false);
            toReceiveSpell.transform.localPosition = Vector3.zero;
            toReceiveSpell.transform.localRotation = Quaternion.identity;
            toReceiveSpell.transform.localScale = Vector3.one;
            toReceiveSpell.Initialize(spellToReceive, null);

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
            if (isSpellUpgrade)
            {
                Debug.Log($"Scoresheet Upgrade: {toUpgradeScore.Score} {toUpgradeSpell.Spell} -> {toReceiveSpell.Spell}");
                int index = toUpgradeScore.Score.Spells.IndexOf(toUpgradeSpell.Spell);
                toUpgradeScore.Score.Spells[index] = toReceiveSpell.Spell;
            }
            else
            {
                List<SpellInstance> spells = new();
                for (int i = 0; i < toReceiveScore.Score.Spells.Count; i++)
                {
                    if (toReceiveScore.Score.Spells[i].Data.Type != SpellType.spell_none)
                    {
                        spells.Add(toReceiveScore.Score.Spells[i]);
                    }
                    else
                    {
                        spells.Add(toUpgradeScore.Score.Spells[i]);
                    }
                }

                ScoreInstance nextScore = ScoreFactory.CreateInstance(toReceiveScore.Score.Data, spells);
                int index = GameInstance.Current.Character.Scoresheet.Scores.IndexOf(toUpgradeScore.Score);
                GameInstance.Current.Character.Scoresheet.Scores[index] = nextScore;
                toUpgradeScore.Score = nextScore;
            }
        }
        
        #region Upgrade Sequence
        
        private void ScoreSelectSequence(Transform toReceiveTransform, 
                                           ScoreButton toUpgradeButton, 
                                           Action onComplete = null)
        {
            foreach (MonoBehaviour mb in offWhileAnimating)
            {
                mb.enabled = false;
            }
            
            Vector3 pos = toReceiveScore.transform.position;

            int siblingIndex = toUpgradeButton.transform.GetSiblingIndex();
            Transform returnParent = toUpgradeButton.transform.parent;
            toUpgradeButton.transform.SetParent(animGroup, true);
            
            Sequence sequence = DOTween.Sequence();
            
            // Selected score to upgrade area
            Vector3 toReceivePos = toReceiveTransform.position;
            Vector3 toUpgradePos = toUpgradeContainer.position;
            Vector3 returnPos = toUpgradeButton.transform.position;
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
            sequence.Insert(returnInsertTime, toUpgradeButton.transform.DOMove(returnPos, returnTime)
                                                            .OnStart(() => RuntimeManager.PlayOneShot(swooshSfx))
                                                            .SetEase(returnEase)
                                                            .SetDelay(0.3f));
            sequence.Insert(returnInsertTime, fadeImage.DOFade(0, fadeTime)
                                                       .SetEase(fadeEase)
                                                       .OnComplete(() => fadeImage.raycastTarget = false));
            
            sequence.AppendInterval(0.5f);

            sequence.OnComplete(() =>
                                {
                                    toUpgradeButton.transform.SetParent(returnParent);
                                    toUpgradeButton.transform.SetSiblingIndex(siblingIndex);
                                    foreach (MonoBehaviour mb in offWhileAnimating)
                                    {
                                        mb.enabled = true;
                                    }
                                    onComplete?.Invoke();
                                });
            sequence.Play();
        }
        
        #endregion
        
        #region Show/Hide

        public void Show(bool force = false, Action onComplete = null)
        {
            if (force)
            {
                root.gameObject.SetActive(true);
                transform.localPosition = Vector3.zero;
                onComplete?.Invoke();
                return;
            }
            
            root.gameObject.SetActive(true);
            transform.DOLocalMove(Vector3.zero, showTime)
                     .SetEase(showEase)
                     .OnComplete(() => onComplete?.Invoke());
        }

        public void Hide(bool force = false, Action onComplete = null)
        {
            if (force)
            {
                transform.localPosition = showHidePosition;
                root.gameObject.SetActive(false);
                onComplete?.Invoke();
                return;
            }
            
            transform.DOLocalMove(showHidePosition, hideTime)
                     .SetEase(hideEase)
                     .OnComplete(() =>
                                 {
                                     root.gameObject.SetActive(false);
                                     onComplete?.Invoke();
                                 });
        }
        
        #endregion
    }
}