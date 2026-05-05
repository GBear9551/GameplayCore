using FightSongSoundSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{

   public static AudioSystem Instance;

   [SerializeField] private GameObjectPool m_AudioGameObjectPool; 
   [SerializeField] private SoundConfigSO m_StartingMusic;

   private void Awake()
   {

       if(Instance != null)
       {
          Destroy(this);
       }
       else
       {
          Instance = this;
          DontDestroyOnLoad(this);
       }

   }

  private void Start()
  {
    PlayMusic(m_StartingMusic);
  }

  public static bool PlayMusic(SoundConfigSO soundConfigSO)
  {

    // Declare and initialize variables
    AudioSource audioSource;

    // Get an audio source game object from the pool
    audioSource = Instance.GetAudioSource();

    // If there exists an audiosource available
    if (audioSource != null)
    {

      // If the audio source is not playing a sound, play one, or check if it is a one shot.

        audioSource.Stop();

        // Use the current sound data config to configure the audio source.
        // Function: AudioSystem.ConfigureAudioSource(AudioSource,SoundConfigSO)
        //AudioSystem.ConfigureAudioSource(audioSource, soundConfig);
        Instance.ConfigureAudioSource(audioSource, soundConfigSO);

        // Play the audio source with the audio clip.
        Instance.PlayAudioSource(audioSource);

      

    }

    return false;
  }

  public static bool PlaySound(SoundConfigSO soundConfigSO)
   {

    // Declare and initialize variables
      AudioSource audioSource;

    // Get an audio source game object from the pool
      audioSource = Instance.GetAudioSource();

    // If there exists an audiosource available
      if (audioSource != null)
      {

        // If the audio source is not playing a sound, play one, or check if it is a one shot. 
        if (!audioSource.isPlaying) // && soundConfigSO.playOverlappingSound == false)
        {

          // Use the current sound data config to configure the audio source.
          // Function: AudioSystem.ConfigureAudioSource(AudioSource,SoundConfigSO)
          //AudioSystem.ConfigureAudioSource(audioSource, soundConfig);
            Instance.ConfigureAudioSource(audioSource, soundConfigSO);

          // Play the audio source with the audio clip.
            Instance.PlayAudioSource(audioSource);

        }
      }

      return false;
   }

   private bool PlayAudioSource(AudioSource audioSource)
   {
      if (audioSource == null) return false;

      if (!audioSource.isPlaying)
      {
        var pooledAudioSource = audioSource.GetComponent<PooledAudioSourceObject>();
        if (pooledAudioSource != null) 
        {
          pooledAudioSource.Play();
        }
        else
        {
         //audioSource.Play();
        } 
       
      }

      return true;
   }

   private AudioSource GetAudioSource()
   { 
      GameObject gameObj = m_AudioGameObjectPool.Pool.Get();
     
      if( gameObj != null )
      {
        return gameObj.GetComponent<AudioSource>();
      }
      return null;
   }

   private bool ConfigureAudioSource(AudioSource audioSource, SoundConfigSO soundConfigSO)
   {

      if( audioSource == null  || soundConfigSO == null) { return false; }

      // Match data from the audioSource to the soundConfigSO 
      audioSource.loop = soundConfigSO.IsAudioLooping();
      audioSource.clip = soundConfigSO.GetAudioClip();
      audioSource.volume = soundConfigSO.GetVolume();
      audioSource.outputAudioMixerGroup = soundConfigSO.GetAudioMixerGroup();
      
      // Is pitch randomized?
      if(soundConfigSO.IsPitchRandomized())
      {
         float randomPitchValMinMaxRange = soundConfigSO.GetPitchRandomizerVal();
         float randomVal = UnityEngine.Random.Range(-randomPitchValMinMaxRange, randomPitchValMinMaxRange);
         audioSource.pitch = soundConfigSO.GetPitch() + randomVal;
      }
      else
      {
        audioSource.pitch = soundConfigSO.GetPitch();
      }

      return true;
   }

   

}
