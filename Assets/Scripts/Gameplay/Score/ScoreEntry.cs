using ProjectYahtzee.Gameplay.Settings;
using TMPro;
using UnityEngine;

namespace ProjectYahtzee.Gameplay.Score
{
    public class ScoreEntry : MonoBehaviour
    {
        [SerializeField]
        private ScoreType type;
        
        [Header("References")]
        
        [SerializeField]
        private TMP_Text tmp;
        
        [SerializeField]
        private TMP_Text score;

        private void OnValidate()
        {
            if (GameplaySettings.Settings.ScoreInformation.TryGetInformation(type, out var information))
            {
                if (tmp)
                {
                    tmp.text = information.LocName.GetLocalizedString();
                }
            }
        }
    }
}
