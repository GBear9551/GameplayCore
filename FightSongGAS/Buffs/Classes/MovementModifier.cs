using UnityEngine;

namespace FightSongGameLogicSystem 
{
    public class MovementModifier : Modifier 
    {
       [SerializeField] private float m_MovementSpeedModifier;

       public float GetMovementModifier()
       {
         return m_MovementSpeedModifier;
       }


      
       
    }
}
