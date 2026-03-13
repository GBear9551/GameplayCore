using UnityEngine;

namespace FightSongGameLogicSystem 
{
     
    public class SingleUseModifier<T> : Modifier
    {

        bool m_Used = false;
        T m_ModifierVal;

        public virtual T GetModifier()
        {
          if (m_Used)
          {
            return default;
          }
          else
          {
            m_Used = true;
            return m_ModifierVal;
          }

        }
     
    }
}
