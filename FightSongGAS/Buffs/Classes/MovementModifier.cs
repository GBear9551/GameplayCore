using UnityEngine;
using System.Collections.Generic;

namespace FightSongGameLogicSystem 
{
    public class MovementModifier : Modifier 
    {
       [SerializeField] private float m_MovementSpeedModifier;


       public MovementModifier(float movementSpeedModifier)
       {
         m_MovementSpeedModifier = movementSpeedModifier; 
       }

       public MovementModifier(MovementModifier other)
       {
          m_MovementSpeedModifier = other.m_MovementSpeedModifier;
          m_From = other.m_From;
          m_Targets = new List<GameObject>(other.m_Targets);
       }

       public float GetMovementModifier()
       {
         return m_MovementSpeedModifier;
       }


      
       
    }
}
