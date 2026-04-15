using UnityEngine;

namespace FightSongGameLogicSystem
{
    public class DamageModifier : Modifier 
    {

       [SerializeField] int m_DamageAmount;

       // Scriptable Object AbilityTypes[]

       
       public DamageModifier(int damageAmount)
       {
          m_DamageAmount= damageAmount;
       }

       public DamageModifier(DamageModifier damageModifier)
       {
          m_DamageAmount = damageModifier.m_DamageAmount;
       }

       public int GetDamageModifier()
       {
          return m_DamageAmount; 
       }


    }
}
