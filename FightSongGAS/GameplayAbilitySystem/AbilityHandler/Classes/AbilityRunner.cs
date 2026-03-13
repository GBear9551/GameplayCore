using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
namespace FightSongGameLogicSystem
{
    public class AbilityRunner : MonoBehaviour
    {

       [SerializeField] List<Ability> m_Ability;

       public void UseAbility(List<GameObject> targets)
       {
          if(m_Ability != null)
          { 
            foreach (var ability in m_Ability)
            {
              ability.Use(this.gameObject, targets);
            }
          }
       }


    }
}
