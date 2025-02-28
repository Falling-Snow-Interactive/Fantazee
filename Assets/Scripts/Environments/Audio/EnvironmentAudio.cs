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

        private EnvironmentType current = EnvironmentType.None;
        
        public void PlayMusic(EnvironmentType environment)
        {
            if (current != environment)
            {
                StopMusic(current);
                if (EnvironmentSettings.Settings.Information.TryGetInformation(environment,
                                                                               out EnvironmentInformation info))
                {
                    eventEmitter.EventReference = info.MusicReference;
                    eventEmitter.Play();
                    current = environment;
                }
            }
        }

        public void StopMusic(EnvironmentType environment)
        {
            if (environment != EnvironmentType.None)
            {
                return;
            }
            
            if (EnvironmentSettings.Settings.Information.TryGetInformation(environment, 
                                                                           out EnvironmentInformation info))
            {
                eventEmitter.EventReference = info.MusicReference;
                eventEmitter.Stop();
            }
        }
    }
}