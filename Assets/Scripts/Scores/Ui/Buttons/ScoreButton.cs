using System;
using DG.Tweening;
using Fantazee.Scores.Instance;
using Fantazee.Spells.Ui;
using Fantazee.Ui;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fantazee.Scores.Ui.Buttons
{
    public class ScoreButton : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        // Constants
        private static Color DefaultBgColor => new(81f/255f, 45f/255f,13f/255f,255f/255f);
        private static Color DefaultOutlineColor => new(127f/255f, 71f/255f,9f/255f,255f/255f);
        
        // Callback
        private Action<ScoreButton> onClick;
        
        // Score
        public ScoreInstance Score { get; set; }

        [Header("Properties")]

        [Header("Colors")]

        [SerializeField]
        private BackgroundColorProperties normalColors = new(DefaultBgColor, DefaultOutlineColor);
        
        [SerializeField]
        private BackgroundColorProperties selectedColors = new(DefaultBgColor, DefaultOutlineColor);
        
        [SerializeField]
        private BackgroundColorProperties disabledColors = new(DefaultBgColor, DefaultOutlineColor);

        [SerializeField]
        private BackgroundColorProperties clickedColors = new(DefaultBgColor, DefaultOutlineColor);
        
        private List<Tweener> colorTweens = new();

        [Header("References")]
        
        [SerializeField]
        private TMP_Text nameText;
        public TMP_Text NameText => nameText;

        [SerializeField]
        protected Button button;
        public Button Button => button;
        
        [SerializeField]
        private List<SpellButton> spells = new();
        public List<SpellButton> Spells => spells;

        [SerializeField]
        private List<BackgroundRef> backgroundRefs = new();

        private void OnValidate()
        {
            foreach (BackgroundRef bg in backgroundRefs)
            {
                if (bg.IsValid)
                {
                    normalColors.Apply(bg);
                }
            }
        }

        private void Reset()
        {
            normalColors = new BackgroundColorProperties(DefaultBgColor, DefaultOutlineColor);
            selectedColors = new BackgroundColorProperties(DefaultBgColor, DefaultOutlineColor);
            disabledColors = new BackgroundColorProperties(DefaultBgColor, DefaultOutlineColor);
            clickedColors = new BackgroundColorProperties(DefaultBgColor, DefaultOutlineColor);

            TryGetComponent(out button);

            foreach (TextMeshProUGUI text in GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (text.name == "score_name")
                {
                    nameText = text;
                    break;
                }
            }

            spells = new List<SpellButton>();
            foreach (SpellButton spell in GetComponentsInChildren<SpellButton>())
            {
                spells.Add(spell);
            }
        }

        private void OnEnable()
        {
            // button.OnSelect();
        }

        public void Initialize(ScoreInstance score, Action<ScoreButton> onClick)
        {
            Score = score;
            this.onClick = onClick;
            
            Debug.Assert(spells.Count <= score.Spells.Count);
            for (int i = 0; i < Spells.Count; i++)
            {
                Spells[i].Initialize(score.Spells[i], null);
            }
            
            UpdateVisuals();
        }
        
        public void OnClick()
        {
            onClick?.Invoke(this);
        }

        public void UpdateVisuals()
        {
            nameText.text = Score.Data.Name;
            button.interactable = true;

            Debug.Assert(spells.Count == Score.Spells.Count, 
                         $"Spells Count ({spells.Count}) != Score Spells Count ({Score.Spells.Count}).", 
                         gameObject);
            for (int i = 0; i < spells.Count; i++)
            {
                spells[i].Initialize(Score.Spells[i], null);
            }
        }

        protected List<int> GetDiceValues()
        {
            List<int> diceValues = new();

            switch (Score)
            {
                case NumberScoreInstance n:
                    diceValues = new List<int>{n.NumberData.Number, 
                                                  n.NumberData.Number, 
                                                  n.NumberData.Number,
                                                  n.NumberData.Number,
                                                  n.NumberData.Number};
                    break;
                case KindScoreInstance k:
                    diceValues = new List<int>();
                    for (int i = 0; i < k.KindData.Kind; i++)
                    {
                        diceValues.Add(6);
                    }

                    while (diceValues.Count < 5)
                    {
                        diceValues.Add(Random.Range(1, 6));
                    }

                    break;
                case RunScoreInstance r:
                    diceValues = new List<int>();
                    for (int i = 0; i < r.RunData.Run; i++)
                    {
                        diceValues.Add(1 + i);
                    }

                    while (diceValues.Count < 5)
                    {
                        diceValues.Add(Random.Range(1, r.RunData.Run));
                    }

                    break;
                case FullHouseScoreInstance f:
                    diceValues = new List<int> { 5,5,5,2,2 };
                    break;
                case ChanceScoreInstance c:
                    diceValues = new List<int>{Random.Range(1, 6), 
                                                  Random.Range(1, 6), 
                                                  Random.Range(1, 6), 
                                                  Random.Range(1, 6), 
                                                  Random.Range(1, 6)};
                    break;
                case FantazeeScoreInstance f:
                    diceValues = new List<int> { 6,6,6,6,6 };
                    break;
                case TwoPairScoreInstance tp:
                    diceValues = new List<int> { 6, 6, 5, 5, Random.Range(1, 5) };
                    break;
                case EvenOddScoreInstance e when !e.EvenOddData.Even:
                    diceValues = new List<int> { 1, 3, 5, 3, 1 };
                    break;
                case EvenOddScoreInstance e when e.EvenOddData.Even:
                    diceValues = new List<int> { 2, 4, 6, 4, 2 };
                    break;
                default:
                    Debug.LogError($"{nameof(Score)} has not been implemented.");
                    break;
            }
            
            return diceValues;
        }

        #region Request Spell
        
        public void RequestSpell(Action<SpellButton> onSpellSelect)
        {
            foreach (SpellButton spell in spells)
            {
                spell.Activate(s => OnSpellSelected(s, onSpellSelect));
            }
        }

        private void OnSpellSelected(SpellButton spell, Action<SpellButton> onSpellSelect)
        {
            foreach (SpellButton s in spells)
            {
                s.Deactivate();
            }

            onSpellSelect?.Invoke(spell);
        }
        
        #endregion
        
        #region Ui Events
        
        public void OnSelect(BaseEventData eventData)
        {
            Debug.Log($"OnSelect: {Score.Data.Name}");
            foreach (var bg in backgroundRefs)
            {
                selectedColors.Apply(bg);
            }
        }

        public void OnDeselect(BaseEventData eventData)
        {
            Debug.Log($"OnDeselect: {Score.Data.Name}");
            foreach (var bg in backgroundRefs)
            {
                normalColors.Apply(bg);
            }
        }
        
        #endregion
    }
}