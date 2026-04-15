using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongStateMachine
{
  public interface ILocomotionState : IState
  {
      void Jump(ILocomotionStateContext stateContext);
      void Fall(ILocomotionStateContext stateContext);
      void Land(ILocomotionStateContext stateContext);
      void Crouch(ILocomotionStateContext stateContext);
  }
}
