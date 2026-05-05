using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace FightSongSoundSystem
{


  [CreateAssetMenu(fileName = "Sound Configuration", menuName = "Scriptable Objects/SoundConfiguration")]
  public class SoundConfigSO : ScriptableObject 
  {

    public enum AudioTypes 
    {
       SFX,
       Music
    }

    [Header("Audio Mixer Track")]
    [SerializeField] AudioTypes m_AudioType;
    [SerializeField] AudioMixerGroup m_AudioMixerGroup;
    

    [Header("Audio Clip")]
    [SerializeField] AudioClip m_AudioClip;

    [Header("Is sound looping?")]
    [SerializeField] bool m_IsLooping;

    [Header("Volume")]
    [SerializeField, Range(0.1f,9.5f)] float m_Volume;

    [Header("Pitch Controls")]
    [SerializeField] bool m_IsPitchRandomized;
    [SerializeField, Range(0.1f, 0.25f)] float m_PitchRandomizationValue;
    [SerializeField] float m_Pitch;

    public float GetPitch()
    { 
       return m_Pitch; 
    }
 
    public float GetPitchRandomizerVal()
    {
       return m_PitchRandomizationValue;
    }

    public AudioMixerGroup GetAudioMixerGroup()
    {
      return m_AudioMixerGroup;
    }

    public AudioClip GetAudioClip()
    {
       return m_AudioClip;
    }

    public bool IsAudioLooping()
    { 
      return m_IsLooping; 
    }

    public AudioTypes GetAudioType()
    { 
      return m_AudioType; 
    }

    public float GetVolume()
    {
      return m_Volume;
    }

    public bool IsPitchRandomized()
    {
      return m_IsPitchRandomized;
    }
   
  }

}
