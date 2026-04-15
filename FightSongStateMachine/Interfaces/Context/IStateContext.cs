using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongStateMachine
{
  public interface IStateContext<TState> where TState : IState
  {
    void SetState(TState newState);
  }
}
