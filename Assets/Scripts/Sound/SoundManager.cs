using System.Collections;
using System.Collections.Generic;
using Scripts.Sound;
using Scripts.Core;
using SpawnSystem;
using UnityEngine;

namespace Scripts.Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField,Header("SFX clips")] private AudioClip[] clips;
        [SerializeField,Header("BG music")] private AudioSource music;
        private Dictionary<string, AudioClip> soundsDict;


        private void Start()
        {
            PlayMusic();

            soundsDict = new Dictionary<string, AudioClip>();

            foreach (var clip in clips)
            {
                soundsDict.Add(clip.name, clip);
            }
        }

        public void PlayMusic(bool loop = true)
        {
            music.loop = loop;
            music.Play();
        }

        public void PlaySFX(string soundName)
        {
            var clip = soundsDict[soundName];
            if (clip != null)
            {
                App.Instance.ObjectPooler.SpawnFromPool(PoolObjectsTag.Sound).GetComponent<SFX>().Play(clip);
            }
        }
    }
}