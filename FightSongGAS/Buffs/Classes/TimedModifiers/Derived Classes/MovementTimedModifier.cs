using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
    public class MovementTimedModifier : TimedModifier 
    {
      // Update termination -> Modifier <- or -> TimedModifier 
      public override void Initialize(GameObject from, List<GameObject> targets, float duration)
      {
        if (duration > m_CurrDuration)
        {
          base.Initialize(from, targets, duration);
        }
      }
    }
}
