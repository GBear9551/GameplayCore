using UnityEngine;

namespace FightSongGameLogicSystem
{
    public class DamageModifier : Modifier 
    {

       [SerializeField] int m_DamageAmount;

       public int GetDamageModifier()
       {
          return m_DamageAmount; 
       }


    }
}
