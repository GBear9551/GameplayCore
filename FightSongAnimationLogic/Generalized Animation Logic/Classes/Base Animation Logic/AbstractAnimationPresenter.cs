using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FightSongGameLogicSystem;
using FightSongStateMachine;

namespace FightSongAnimationSystem
{

  [RequireComponent(typeof(LocomotionStateMachine))]
  public abstract class AbstractAnimationPresenter : MonoBehaviour, IAnimationPresenter
  {

    [SerializeField] protected Effect m_WalkingEffect;
    [SerializeField] protected ParticleSystem m_WalkingSimpleParticleSystem;
    [SerializeField] protected Effect m_JumpingEffect;
    [SerializeField] protected ParticleSystem m_JumpingSimpleParticleSystem;

    protected LocomotionStateMachine m_LocomotionStateMachine;
    

    protected virtual void Awake()
    {
        m_LocomotionStateMachine = GetComponent<LocomotionStateMachine>();

    }

    // If the locomotion state is walking then play the walking effect
    protected virtual void  PlayWalkEffect()
    {

      if (m_WalkingEffect != null)
      {
        m_WalkingEffect.PlayVFX();
        m_WalkingEffect.PlaySFX();
      }
      if (m_WalkingSimpleParticleSystem != null)
      {
        if (m_WalkingSimpleParticleSystem.isPlaying == false)
        {
          m_WalkingSimpleParticleSystem.Play();
        }
      }
    }

    // If the locomotion state is Jumping then play the jump effect
    protected virtual void PlayJumpEffect()
    {

      if (m_JumpingEffect != null)
      {
        m_JumpingEffect.PlayVFX();
        m_JumpingEffect.PlaySFX();
      }

      if(m_JumpingSimpleParticleSystem != null)
      {
         if(m_JumpingSimpleParticleSystem.isPlaying == false)
         {
            m_JumpingSimpleParticleSystem.Play();
         }
      }

    }



  }
}
