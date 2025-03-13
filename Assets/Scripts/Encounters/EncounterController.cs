using Fantazee.Environments;
using Fantazee.Environments.Settings;
using Fantazee.Instance;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Encounters
{
    public class EncounterController : MonoBehaviour
    {
        private EncounterData encounter;
        
        [Header("References")]

        [SerializeField]
        private Image backgroundImage;

        [SerializeField]
        private TMP_Text headerText;

        private void Start()
        {
            if (EnvironmentSettings.Settings.TryGetEnvironment(GameInstance.Current.Map.Environment, 
                                                               out EnvironmentData env))
            {
                encounter = env.GetEncounter();
                backgroundImage.sprite = env.GetBackground();
                headerText.text = encounter.Name;
            }
        }
    }
}
