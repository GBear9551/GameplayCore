using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongStateMachine
{
  public abstract class AbstractState : IState
  {

    /* Thinking about providing the player with the ability to level up their states, gain access to double jump via number of times single jump is used.
    public int m_CurrentLevel = 0;
    public int m_MaxLevel = 10;
    */

    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }
  }
}
