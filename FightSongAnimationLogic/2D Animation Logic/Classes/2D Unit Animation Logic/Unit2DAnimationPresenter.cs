using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongAnimationSystem
{
  public class Unit2DAnimationPresenter : UnitAnimationPresenter
  {

    [Header("Assuming looping simple walking animation and non-looping simple jumping vfx.")]
    private Rigidbody2D m_RigidBody2D;

    protected override void Awake()
    {
      base.Awake();
      m_RigidBody2D = GetComponent<Rigidbody2D>();
    }

 
    private void Update() 
    {
      if (m_RigidBody2D != null && m_CanWalk)
      {
        if (m_RigidBody2D.velocity.magnitude > 0)
        {
          PlayWalkEffect();
        }
        else
        {
          StopWalkEffect();
        }
      }
      else
      {
        StopWalkEffect(); 
      }
    }

    private void StopWalkEffect()
    {
      if (m_WalkingSimpleParticleSystem.isPlaying)
      {
        m_WalkingSimpleParticleSystem.Stop();
      }
    }

    protected override void PlayWalkEffect()
    {
      base.PlayWalkEffect();
    }
  }
}

