using System;
using UnityEngine;

namespace Contra
{
      public class AudioManager : MonoBehaviour
      {
            public static AudioManager Instance;

            [SerializeField] private AudioSource _audioSourceMusic;
            [SerializeField] private AudioSource[] _audioSourceSFX;

            [SerializeField] private AudioWithName[] _audioWithName;

            private void Awake()
            {
                  if (Instance == null)
                  {
                        Instance = this;
                        DontDestroyOnLoad(gameObject);
                  }
                  else
                  {
                        Destroy(gameObject);
                  }
            }

            public void Play(AudioName audio)
            {
                  foreach (var currentAaudio in _audioWithName)
                  {
                        if (currentAaudio.Name == audio)
                        {
                              if (currentAaudio.Type == AudioType.Music)
                              {
                                    PlayMusic(currentAaudio.Clip);
                              }
                              else if (currentAaudio.Type == AudioType.SFX)
                              {
                                    PlaySFX(currentAaudio.Clip);
                              }
                        }
                  }
            }

            private void PlaySFX(AudioClip audioClip)
            {
                  foreach (var audioSource in _audioSourceSFX)
                  {
                        if (!audioSource.isPlaying)
                        {
                              audioSource.clip = audioClip;
                              audioSource.Play();
                              break;
                        }
                  }
            }

            private void PlayMusic(AudioClip audioClip)
            {
                  _audioSourceMusic.clip = audioClip;
                  _audioSourceMusic.Play();
            }
      }

      [Serializable]
      public struct AudioWithName
      {
            public AudioName Name;
            public AudioType Type;
            public AudioClip Clip;
      }

      public enum AudioName
      {
            Music_Intro,
            Music_Level1,
            SFX_PlayerDie,
            SFX_EnemyDie,
            SFX_Boss_Defeated,
            SFX_Boss_TakeHit,
            SFX_ButtonClick,
            SFX_Power_Take,
            SFX_Weapon_M,
            SFX_Weapon_S,
      }

      public enum AudioType
      {
            Music,
            SFX
      }
}
