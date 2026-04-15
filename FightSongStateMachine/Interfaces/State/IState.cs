using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FightSongStateMachine
{
  public interface IState
  {

    void OnExit();
    void OnEnter();

  }
}
