using UnityEngine;

namespace FightSongGameLogicSystem 
{
     
    public class SingleUseModifier<T> : Modifier
    {

        bool m_Used = false;
        T m_ModifierVal;

        public void SetModifier(T modifierVal)
        {
          m_ModifierVal = modifierVal;
        }
 
        public virtual T GetModifier()
        {
          if (m_Used)
          {
            
            return default;
          }
          else
          {
            m_Used = true;
            //Unregister from targets
            return m_ModifierVal;
          }

        }
     
    }
}
