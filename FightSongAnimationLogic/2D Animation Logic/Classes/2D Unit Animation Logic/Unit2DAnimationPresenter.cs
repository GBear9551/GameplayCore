using FightSongGameLogicSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongAnimationSystem
{
  public class Unit2DAnimationPresenter : UnitAnimationPresenter
  {

    [Header("Assuming looping simple walking animation and non-looping simple jumping vfx.")]
    [SerializeField] protected float m_LandingForceThreshold; // Used to detect/branch and cause a screen shake based on how hard the 2d unit rb lands. (velocity alias as force).
    protected Rigidbody2D m_RigidBody2D;
    protected Unit2DPlatformerMovement m_Unit2DPlatformerMovement;


    protected override void Awake()
    {
      base.Awake();
      m_Unit2DPlatformerMovement = GetComponent<Unit2DPlatformerMovement>();
      m_RigidBody2D = GetComponent<Rigidbody2D>();

      m_Unit2DPlatformerMovement.OnBigLandEvent += PlayLandEffect;

    }

    protected override void OnDestroy()
    {
 
      base.OnDestroy();
      m_Unit2DPlatformerMovement.OnBigLandEvent -= PlayLandEffect;
    }

    private void Update() 
    {

      // To lean in air or not? To canwalk particle or not?
      if (m_RigidBody2D != null && m_CanWalk)
      {
        if (m_RigidBody2D.velocity.x != 0)
        {
          PlayWalkEffect();
        }
        else
        {
          StopWalkEffect();
        }
      }
      else // we can not walk and may be in the air
      {
        PlayInAirEffect();
      }


      if(m_RigidBody2D != null)
      {
        if(m_RigidBody2D.velocity.y < 0f)
        {
          StopJumpEffect();
        }
      }

      //if(m_RigidBody2D != null && m_LocomotionStateMachine.OnInAirEvent)

    }
   
    protected float GetLandingSpeed()
    {

      float landingSpeed = 0f;

      if (m_RigidBody2D != null)
      {
        landingSpeed = Mathf.Abs(m_RigidBody2D.velocity.y);
        float landingForce = Mathf.Abs(m_RigidBody2D.totalForce.y);
      }

      return landingSpeed;
    }

    protected override void PlayLandEffect()
    {

      base.PlayLandEffect();
      m_CinemachImpulseSource.GenerateImpulse(m_ImpulseForce);

    }

    protected virtual void PlayInAirEffect()
    {

    }

    protected virtual void StopWalkEffect()
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

