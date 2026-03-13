using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace FightSongGameLogicSystem
{
  public abstract class Effect : MonoBehaviour, IEffect
  {

      [SerializeField] List<GameObject> m_VFXList;
      [SerializeField] List<AudioClip> m_AudioClipList;

      public virtual void PlaySFX()
      {
      }

      public virtual void PlayVFX()
      {
      }
  }
}
