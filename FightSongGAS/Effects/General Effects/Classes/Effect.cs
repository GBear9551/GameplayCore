using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using FightSongLoggingSystem;
using FightSongSoundSystem;

namespace FightSongGameLogicSystem
{
  public abstract class Effect : MonoBehaviour, IEffect
  {

     [SerializeField] EffectConfigSO m_EffectConfigSO;

     private List<SoundConfigSO> m_Sounds;

    public void Awake()
    {
        m_Sounds = m_EffectConfigSO.GetSounds();
    }


    public virtual void PlaySFX()
    {

      // Declare and initialize variables
      List<SoundConfigSO> soundConfigs = null;

      // Grab the list of sound data
      soundConfigs = m_EffectConfigSO.GetSounds();

      // If the list of sound data exists
      if (soundConfigs != null)
      {

        // Loop for each sound data, play the audio clip associated with the sound data config.
        foreach (SoundConfigSO soundConfig in soundConfigs)
        {


          // Play sound via the audio system.
          AudioSystem.PlaySound(soundConfig);

            // Grab an audio source from the audio system
            //audioSource = AudioSystem.Instance.GetAudioSource();

            // Use the current sound data config to configure the audio source.
            // Function: AudioSystem.ConfigureAudioSource(AudioSource,SoundConfigSO)
            //AudioSystem.ConfigureAudioSource(audioSource, soundConfig);

            // Play the audio source with the audio clip.
            //AudioSystem.PlayAudioSource(audioSource);
          
        }

      }



    }


   /* public virtual void PlaySFX()
      { 

         AudioSource audioSource = m_EffectConfigSO.GetAudioSource();
         List<AudioClip> audioClipList = m_EffectConfigSO.GetAudioClipList();

         if(audioSource != null)
         {

          if(audioClipList != null) 
          {
            foreach (AudioClip clip in audioClipList)
            {
              audioSource.clip = clip;
              if (audioSource.isPlaying == false)
              {
                audioSource.Play();
              }
            }
          }

         }
      }*/



      public virtual GameObjectPool PlayVFX()
      {
        // Might/active game object list promise
        GameObjectPool vfxPool = CreateVFXGameObjects();
        GameObject vfx = null;

        // Get a vfx from the pool
        if (vfxPool != null)
        {
          vfx = vfxPool.Pool.Get();
        }

        // Get color of this entity

        // Set the color of Particle System's main module
        if (vfx != null)
        {
          vfx.transform.position = transform.position;
          var ps = vfx.GetComponent<PooledParticleGameObject>();
          ps.PlayPooledParticle();
        }

          return vfxPool;

      }

      public virtual GameObjectPool CreateVFXGameObjects()
      {
         var vfxPool = GameObjectPool.FindOrCreateAndRegisterPool(m_EffectConfigSO.GetGameObjectPoolPrefab());
         
         Vector3 vfxScale = m_EffectConfigSO.GetVFXScale();
         Transform vfxTransformOnPlay = m_EffectConfigSO.GetTransformOnPlay();

         if(vfxPool != null)
         {
            // Need to object pool vfx instances TODO
            if(vfxPool.Pool == null)
            {
               vfxPool.Initialize();
            }
            var vfx  = vfxPool.Pool.Get();


          if (vfx != null)
          {
            vfx.transform.localScale = vfxScale;
            vfx.transform.parent = vfxPool.gameObject.transform;

            // assuming the effect is on the caster.
            if (vfxTransformOnPlay != null)
            {
              vfx.transform.position = vfxTransformOnPlay.position;
            }
            else
            {
              vfx.transform.position = transform.position;
            }



            var pooledGO = vfx.GetComponent<PooledParticleGameObject>();

            if (pooledGO != null)
            {
              pooledGO.ReturnToPool();
            }

          }
            
         }

           
           return  vfxPool;
      }
  }
}
