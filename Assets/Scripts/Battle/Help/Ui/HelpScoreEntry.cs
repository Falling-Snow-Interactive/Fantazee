using Fantazee.Scores.Instance;
using TMPro;
using UnityEngine;

namespace Fantazee.Battle.Help.Ui
{
    public class HelpScoreEntry : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text nameText;

        [SerializeField]
        private TMP_Text descText;

        public void Initialize(ScoreInstance score)
        {
            nameText.text = score.Data.Name;
            descText.text = score.Data.Description;
        }
    }
}
