using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace FightSongGameLogicSystem
{
  public abstract class Effect : MonoBehaviour, IEffect
  {

      [SerializeField] List<GameObject> m_VFXList;
      [SerializeField] List<AudioClip> m_AudioClipList;
      [SerializeField] AudioSource m_AudioSource;
      [SerializeField] Transform m_VFXTransformOnPlay;
      [SerializeField] Vector3 m_VFXScale;

      public virtual void PlaySFX()
      { 
         if(m_AudioSource != null)
         {

          if(m_AudioClipList != null) 
          {
            foreach (AudioClip clip in m_AudioClipList)
            {
              m_AudioSource.clip = clip;
              if (m_AudioSource.isPlaying == false)
              {
                m_AudioSource.Play();
              }
            }
          }

         }
      }

      public virtual void PlayVFX()
      {
         if(m_VFXList != null)
         {
            foreach(GameObject obj in m_VFXList)
            {
               var vfx  = Instantiate(obj);
               vfx.transform.localScale = m_VFXScale;
               // assuming the effect is on the caster.
               vfx.transform.position = m_VFXTransformOnPlay.position;
 
               var particleSystem = vfx.GetComponent<ParticleSystem>();
               particleSystem?.Play();
            }
         }
      }
  }
}
