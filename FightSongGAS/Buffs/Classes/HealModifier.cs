using UnityEngine;

namespace FightSongGameLogicSystem
{
    public class HealModifier : Modifier 
    {

       [SerializeField] private SingleUseModifier<int> m_SingleUseModifier;

       public int GetHealModifier()
       { 
         return m_SingleUseModifier.GetModifier(); 
       }

    }
}
