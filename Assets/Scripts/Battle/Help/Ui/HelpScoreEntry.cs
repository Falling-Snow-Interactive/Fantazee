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

        public void Initialize(Scores.Score score)
        {
            nameText.text = score.Information.LocName.GetLocalizedString();
            descText.text = score.Information.LocDesc.GetLocalizedString();
        }
    }
}
