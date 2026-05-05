using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FightSongAnimationSystem
{
  public class UnitAnimationPresenter : AbstractAnimationPresenter
  {

    [SerializeField] protected float m_ImpulseForce = 0.2f;
    protected bool m_CanWalk = false;
    

    protected override void Awake()
    {
      base.Awake();

      m_LocomotionStateMachine.OnJumpEvent.AddListener(PlayJumpEffect);
      m_LocomotionStateMachine.OnGroundedEvent.AddListener(GroundedAndCanWalk);
      m_LocomotionStateMachine.OnInAirEvent.AddListener(InAirCantWalk);
     // m_LocomotionStateMachine.OnGroundedEvent.AddListener(PlayLandEffect); // Must be either Rigidbody 2d or 3d generalizable. Moved to Unit 2D & Unit 3D AnimPresenter
    }

    protected virtual void OnDestroy()
    {
      m_LocomotionStateMachine.OnJumpEvent?.RemoveListener(PlayJumpEffect);
      m_LocomotionStateMachine.OnGroundedEvent?.RemoveListener(GroundedAndCanWalk);
      m_LocomotionStateMachine.OnInAirEvent?.RemoveListener(InAirCantWalk);
      //m_LocomotionStateMachine.OnGroundedEvent?.RemoveListener(PlayLandEffect);
    }

    protected virtual void PlayLandEffect()
    {
      
    }

    protected override void PlayJumpEffect()
    {
       base.PlayJumpEffect();
    }

    protected virtual void GroundedAndCanWalk()
    {
      m_CanWalk = true;
    }

    public void InAirCantWalk()
    {
      m_CanWalk = false;
    }

    protected override void PlayWalkEffect()
    {
      base.PlayWalkEffect();
    }


  }
}
