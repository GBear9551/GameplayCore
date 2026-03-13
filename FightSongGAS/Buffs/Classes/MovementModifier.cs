using UnityEngine;

namespace FightSongGameLogicSystem 
{
    public class MovementModifier : Modifier 
    {
       [SerializeField] private float m_MovementSpeed;

       public float GetMovementModifier()
       {
         return m_MovementSpeed;
       }


       public override void Apply()
       {
           
       }

       public override void Remove()
       {

       }
      
       
    }
}
