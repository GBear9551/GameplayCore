using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
namespace FightSongGameLogicSystem
{
    public class AbilityRunner : MonoBehaviour
    {

       [SerializeField] List<AbstractAbility> m_Abilities;

       private GameObject m_From;

       public void UseAbility(List<GameObject> targets)
       {
          if(m_Abilities != null)
          { 
            foreach (var ability in m_Abilities)
            {

              // If class member from is not set, then assume the ability is being casted from the current unity object
              // Warning, if from is not set, the ability could come from a scriptable object containing
              // the ability runner's data.
              if (m_From == null)
              {
                List<IModifier> modifiersCreated = ability.Use(this.gameObject, targets);
              }
              else
              {
                List<IModifier> modifiersCreated = ability.Use(m_From, targets);
              }
              // TODO: Add modifiersCreated to the modifiers created by the ability runner. 

            }
          }
       }

       public void SetFrom(GameObject from)
       { 
         m_From = from; 
       }


    }
}
