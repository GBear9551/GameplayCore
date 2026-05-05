using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
    public class MovementTimedModifier : TimedModifier, IMovementModifier 
    {


      [SerializeField] private float m_MovementSpeedModifier;

 

      public void Initialize(GameObject from, List<GameObject> targets, float duration, float speedModificationAmount)
      {
          Initialize(from, targets, duration);
          m_MovementSpeedModifier = speedModificationAmount;
      }

      // Update termination -> Modifier <- or -> TimedModifier 
      public override void Initialize(GameObject from, List<GameObject> targets, float duration)
      {
        if (duration > m_CurrDuration)
        {
          base.Initialize(from, targets, duration);
        }
      }

    public float GetMovementModifier()
    {
      return m_MovementSpeedModifier;
    }
  }
}
