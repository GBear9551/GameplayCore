using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
  public class KnockBackModifier : TimedModifier, IStunMovementModifier 
  {
    public override void Initialize(GameObject from, List<GameObject> targets, float duration)
    {
      if (duration > m_CurrDuration)
      {
        base.Initialize(from, targets, duration);
      }
    }
  }
}
