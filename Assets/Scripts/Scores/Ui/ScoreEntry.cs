using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle;
using Fantazee.Battle.Score;
using Fantazee.Battle.Settings;
using Fantazee.Items.Dice.Information;
using Fantazee.Items.Dice.Settings;
using Fantazee.Scores.Information;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fantazee.Scores.Ui
{
    public class ScoreEntry : MonoBehaviour
    {
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

        [FormerlySerializedAs("spellImage")]
        [SerializeField]
        private Image spellIcon;

        [SerializeField]
        private GameObject tooltip;

        [SerializeField]
        private TMP_Text tooltipName;

        [SerializeField]
        private TMP_Text tooltipDesc;
        
        private ScoreInformation information;

        private void Awake()
        {
            tooltip.SetActive(false);
        }

        private void OnEnable()
        {
            if (score != null)
            {
                score.DieAdded += OnDieAdded;
            }
        }

        private void OnDisable()
        {
            if (score != null)
            {
                score.DieAdded -= OnDieAdded;
            }
        }

        public void Initialize(BattleScore score)
        {
            this.score = score;

            scoreText.text = "";
            if (BattleSettings.Settings.ScoreInformation.TryGetInformation(score.Score.Type, out information))
            {
                nameText.text = information.LocName.GetLocalizedString();
                for (int i = 0; i < diceImages.Count; i++)
                {
                    ShowInSlot(i, 0);
                }
            }

            spellIcon.sprite = score.SpellData.Icon;
            
            tooltip.SetActive(false);
            tooltipName.text = score.SpellData.LocName.GetLocalizedString();
            tooltipDesc.text = score.SpellData.LocDesc.GetLocalizedString();
            
            score.DieAdded += OnDieAdded;
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
            
            scoreContainer.transform.DOPunchScale(BattleSettings.Settings.SquishAmount, 
                                                  BattleSettings.Settings.SquishTime,
                                                  10,
                                                  1f)
                          .SetEase(BattleSettings.Settings.SquishEase);

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
            tooltip.SetActive(true);
            tooltip.transform.DOLocalMoveY(tooltipOffset, tooltipTime).SetEase(tooltipEase);
        }

        public void HideTooltip()
        {
            tooltip.transform.DOLocalMoveY(0, tooltipTime)
                   .SetEase(tooltipEase)
                   .OnComplete(() => tooltip.SetActive(false));
        }

        public void OnClick()
        {
            BattleController.Instance.SelectScoreEntry(this);
        }
    }
}
