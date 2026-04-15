using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FightSongStateMachine;
namespace FightSongStateMachine
{


  

  public abstract class AbstractLocomotionState : AbstractState, ILocomotionState
  {


    public virtual void Crouch(ILocomotionStateContext stateContext)
    {
      stateContext.SetState(new CrouchingState());
    }

    public virtual void Fall(ILocomotionStateContext stateContext)
    {
      stateContext.SetState(new InAirState()); 
    }

    public virtual void Jump(ILocomotionStateContext stateContext)
    {
      stateContext.SetState(new InAirState()); 
    }

    public virtual void Land(ILocomotionStateContext stateContext)
    {
      stateContext.SetState(new GroundedState()); 
    }
  }

  public class CrouchingState : AbstractLocomotionState 
  {

    public override void Crouch(ILocomotionStateContext stateContext)
    {
        stateContext.SetState(new GroundedState()); 
    }

    public override void Fall(ILocomotionStateContext stateContext)
    {
        stateContext.SetState(new InAirState());
    }

    public override void Jump(ILocomotionStateContext stateContext)
    {
        stateContext.SetState(new InAirState());
    }

    public override void Land(ILocomotionStateContext stateContext)
    {

    }

    public override void OnEnter()
    {
      Debug.Log("Entered Crouching State");
    }

    public override void OnExit()
    {
      Debug.Log("Exited Crouching State");
    }


  }

  public class InAirState : AbstractLocomotionState
  {
    public override void Crouch(ILocomotionStateContext stateContext)
    {

    }

    public override void Fall(ILocomotionStateContext stateContext)
    {

    }

    public override void Jump(ILocomotionStateContext stateContext)
    {

    }

    public override void Land(ILocomotionStateContext stateContext)
    {
       stateContext.SetState(new GroundedState());
    }

    public override void OnEnter()
    {
      Debug.Log("Entered  InAir State");
    }

    public override void OnExit()
    {
      Debug.Log("Exited InAir State");
    }

  }

  public class GroundedState : AbstractLocomotionState
  {
    public override void Crouch(ILocomotionStateContext stateContext)
    {
       stateContext.SetState(new CrouchingState());
    }

    public override void Fall(ILocomotionStateContext stateContext)
    {
       stateContext.SetState(new InAirState());
    }

    public override void Jump(ILocomotionStateContext stateContext)
    {
       stateContext.SetState(new InAirState());
    }

    public override void Land(ILocomotionStateContext stateContext)
    {

    }

    public override void OnEnter()
    {
      Debug.Log("Entered Grounded State");
    }

    public override void OnExit()
    {
      Debug.Log("Exited Grounded State");
    }

  }

}
