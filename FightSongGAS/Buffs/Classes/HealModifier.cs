using UnityEngine;

namespace FightSongGameLogicSystem
{
    public class HealModifier : Modifier 
    {

       [SerializeField] private int m_HealAmount;


       public HealModifier(int healAmount)
       {
          m_HealAmount = healAmount;
       }

       public HealModifier(HealModifier other)
       {
         m_HealAmount = other.m_HealAmount;
       }

       public int GetHealModifier()
       { 
         return m_HealAmount;
       }

    }
}
