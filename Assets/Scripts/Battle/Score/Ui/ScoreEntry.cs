using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Settings;
using Fantazee.Dice.Settings;
using Fantazee.Items.Dice.Information;
using Fantazee.Scores.Information;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fantazee.Battle.Score.Ui
{
    public class ScoreEntry : MonoBehaviour
    {
        private Action<ScoreEntry> onSelect;
        
        [SerializeReference]
        private BattleScore score;
        public BattleScore Score => score;

        [Header("Information")]

        [SerializeField]
        private float tooltipOffset = 100f;
        
        [SerializeField]
        private float tooltipTime = 5f;
        
        [SerializeField]
        private Ease tooltipEase = Ease.OutBounce;
        
        [Header("References")]
        
        [SerializeField]
        private TMP_Text nameText;
        
        [FormerlySerializedAs("valueText")]
        [SerializeField]
        private TMP_Text scoreText;
        
        [FormerlySerializedAs("valueContainer")]
        [SerializeField]
        private Transform scoreContainer;

        [SerializeField]
        private Button button;

        [SerializeField]
        private List<Image> diceImages = new();
        public List<Image> DiceImages => diceImages;
        
        [SerializeField]
        protected List<Image> spellIcons = new();
        public List<Image> SpellIcons => spellIcons;

        [FormerlySerializedAs("tooltip")]
        [SerializeField]
        private GameObject tooltipGroup;

        [SerializeField]
        private Transform tooltipRoot;

        [SerializeField]
        private TMP_Text tooltipName;

        [SerializeField]
        private TMP_Text tooltipDesc;
        
        private ScoreInformation information;

        private void Awake()
        {
            tooltipGroup.SetActive(false);
        }

        private void OnEnable()
        {
            if (score != null)
            {
                score.DieAdded += OnDieAdded;
                score.ScoreReset += OnScoreReset;
            }
        }

        private void OnDisable()
        {
            if (score != null)
            {
                score.DieAdded -= OnDieAdded;
                score.ScoreReset -= OnScoreReset;
            }
        }

        public virtual void Initialize(BattleScore score, Action<ScoreEntry> onSelect)
        {
            this.score = score;
            this.onSelect = onSelect;

            scoreText.text = "";
            if (BattleSettings.Settings.ScoreInformation.TryGetInformation(score.Score.Type, out information))
            {
                nameText.text = information.LocName.GetLocalizedString();
                for (int i = 0; i < diceImages.Count; i++)
                {
                    ShowInSlot(i, 0);
                }
            }

            for (int i = 0; i < spellIcons.Count; i++)
            {
                spellIcons[i].sprite = score.Spells[i].Data.Icon;
            }

            tooltipGroup.SetActive(false);
            // tooltipName.text = score.SpellData.LocName.GetLocalizedString();
            // tooltipDesc.text = score.SpellData.LocDesc.GetLocalizedString();
            
            score.DieAdded += OnDieAdded;
            score.ScoreReset += OnScoreReset;
        }

        private void ShowInSlot(int index, int value)
        {
            if (diceImages.Count >= index &&
                DiceSettings.Settings.SideInformation.TryGetInformation(value, out SideInformation info))
            {
                diceImages[index].sprite = info.Sprite;
                diceImages[index].transform.parent.DOPunchScale(Vector3.one * 0.1f, 0.2f, 2, 0.5f);
            }
        }

        public int FinalizeScore()
        {
            int s = score.Calculate();
            button.interactable = false;
            scoreText.text = s.ToString();
            RuntimeManager.PlayOneShot(BattleSettings.Settings.ScoreSfx);
            scoreContainer.transform.DOPunchScale(DiceSettings.Settings.SquishAmount, 
                                                  DiceSettings.Settings.SquishTime,
                                                  10,
                                                  1f)
                          .SetEase(DiceSettings.Settings.SquishEase);

            return s;
        }
        
        private void OnDieAdded()
        {
            for (int i = 0; i < score.Dice.Count; i++)
            {
                ShowInSlot(i, score.Dice[i].Value);
            }
        }

        public void ShowTooltip()
        {
            return;
            DOTween.Complete(tooltipGroup);
            tooltipGroup.SetActive(true);
            Vector3 pos = tooltipRoot.transform.localPosition;
            pos.y = 0;
            tooltipRoot.transform.localPosition = pos;
            tooltipRoot.transform.DOLocalMoveY(tooltipOffset, tooltipTime).SetEase(tooltipEase);
        }

        public void HideTooltip()
        {
            return;
            DOTween.Complete(tooltipRoot);
            Vector3 pos = tooltipRoot.transform.localPosition;
            pos.y = tooltipOffset;
            tooltipRoot.transform.localPosition = pos;
            tooltipRoot.transform.DOLocalMoveY(0, tooltipTime)
                       .SetEase(tooltipEase)
                       .OnComplete(() => tooltipGroup.SetActive(false));
        }

        public void OnClick()
        {
            onSelect?.Invoke(this);
        }

        private void OnScoreReset()
        {
            for (int i = 0; i < diceImages.Count; i++)
            {
                int v = score.Dice.Count > i ? score.Dice[i].Value : 0;
                ShowInSlot(i, v);
            }

            button.interactable = score.Dice.Count == 0;
            scoreText.text = score.Dice.Count == 0 ? "" : score.Calculate().ToString();
        }
    }
}
