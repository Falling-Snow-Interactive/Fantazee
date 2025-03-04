using System.Collections;
using System.Collections.Generic;
using Fantazee.Audio.Information;
using Fantazee.Audio.Settings;
using FMODUnity;
using Fsi.Gameplay;
using UnityEngine;

namespace Fantazee.Audio
{
    public class MusicController : MbSingleton<MusicController>
    {
        [SerializeField]
        private StudioEventEmitter eventEmitter;

        private readonly Dictionary<MusicId, MusicInstance> instances = new();
        private MusicInstance currentMusic;

        
        public void PlayMusic(MusicId musicId)
        {
            if (currentMusic != null && currentMusic.Id == musicId)
            {
                return;
            }
            
            currentMusic?.Stop();
            
            if (!instances.TryGetValue(musicId, out var instance))
            {
                instance = CreateInstance(musicId);
                instances.Add(musicId, instance);
            }
            
            currentMusic = instance;
            currentMusic.Play();
        }

        private MusicInstance CreateInstance(MusicId musicId)
        {
            MusicInstance instance = new(musicId);
            return instance;
        }
    }
}