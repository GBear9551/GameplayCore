using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
namespace FightSongGameLogicSystem
{
    public class AbilityRunner : MonoBehaviour
    {

       [SerializeField] List<AbstractAbility> m_Abilities;

       public void UseAbility(List<GameObject> targets)
       {
          if(m_Abilities != null)
          { 
            foreach (var ability in m_Abilities)
            {

              List<IModifier> modifiersCreated = ability.Use(this.gameObject, targets);
              
              // TODO: Add modifiersCreated to the modifiers created by the ability runner. 

            }
          }
       }


    }
}
