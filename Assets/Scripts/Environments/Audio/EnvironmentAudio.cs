using Fantazee.Battle.Environments;
using Fantazee.Environments.Information;
using Fantazee.Environments.Settings;
using FMODUnity;
using UnityEngine;

namespace Fantazee.Environments.Audio
{
    public class EnvironmentAudio : MonoBehaviour
    {
        [SerializeField]
        private StudioEventEmitter eventEmitter;
        
        public void PlayMusic(EnvironmentType environment)
        {
            if (EnvironmentSettings.Settings.Information.TryGetInformation(environment, 
                                                                           out EnvironmentInformation info))
            {
                eventEmitter.EventReference = info.MusicReference;
                eventEmitter.Play();
            }
        }

        public void StopMusic(EnvironmentType environment)
        {
            if (EnvironmentSettings.Settings.Information.TryGetInformation(environment, 
                                                                           out EnvironmentInformation info))
            {
                eventEmitter.EventReference = info.MusicReference;
                eventEmitter.Stop();
            }
        }
    }
}