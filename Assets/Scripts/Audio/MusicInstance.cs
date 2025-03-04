using Fantazee.Audio.Information;
using Fantazee.Audio.Settings;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.Playables;

namespace Fantazee.Audio
{
    public class MusicInstance
    {
        public MusicId Id { get; }

        private EventInstance music;
        
        public MusicInstance(MusicId musicId)
        {
            Id = musicId;
            if (MusicSettings.Settings.Information.TryGetInformation(musicId, out MusicInformation info))
            {
                music = RuntimeManager.CreateInstance(info.Music);
            }
        }

        public void Play()
        {
            music.start();
        }

        public void Stop()
        {
            music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}