using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongAnimationSystem
{
  public class UnitAnimationPresenter : AbstractAnimationPresenter
  {

    protected bool m_CanWalk = false;
    

    protected override void Awake()
    {
      base.Awake();

      m_LocomotionStateMachine.OnJumpEvent.AddListener(PlayJumpEffect);
      m_LocomotionStateMachine.OnGroundedEvent.AddListener(GroundedAndCanWalk);
      m_LocomotionStateMachine.OnInAirEvent.AddListener(InAirCantWalk);
    }

    protected override void PlayJumpEffect()
    {


       base.PlayJumpEffect();

    }

    public void GroundedAndCanWalk()
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
