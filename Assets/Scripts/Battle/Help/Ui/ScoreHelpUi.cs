using Fantazee.Instance;
using Fantazee.Scores.Instance;

namespace Fantazee.Battle.Help.Ui
{
    public class ScoreHelpUi : MonoBehaviour
    {
        [SerializeField]
        private List<HelpScoreEntry> entries = new();

        private void Start()
        {
            List<ScoreInstance> scores = GameInstance.Current.Character.Scoresheet.Scores;
            Debug.Assert(entries.Count <= scores.Count);
            for (int i = 0; i < entries.Count; i++)
            {
                HelpScoreEntry entry = entries[i];
                entry.gameObject.SetActive(true);
                entry.Initialize(scores[i]);
            }
        }
    }
}