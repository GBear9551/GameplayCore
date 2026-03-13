using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem 
{
    public class Heal : Ability
    {

       [SerializeField] int m_HealAmount;
       [SerializeField] List<HealModifier> m_HealModifiers;

        public override void Use(GameObject from, List<GameObject> targets)
        {
          if (targets != null)
          {

            // Apply ability tracking through IModifiable
            foreach (GameObject target in targets)
            {
              var targetIsAUnit = target.GetComponent<Unit>();
              if (targetIsAUnit != null)
              {
                if (targetIsAUnit.m_Modifiers == null)
                {
                  targetIsAUnit.m_Modifiers = new List<IModifier>();
                }

                if(m_HealModifiers != null)
                {
                  foreach(HealModifier healModifier in m_HealModifiers)
                  { 
                    healModifier.SetFrom(from);
                    healModifier.SetTargets(targets);
                    targetIsAUnit.m_Modifiers.Add(healModifier);
                  }
                }
                    targetIsAUnit.Heal( m_HealAmount, this.gameObject );
                   
                
              }
            }

          }
          else
          {
            Debug.LogError( " No targets to heal, targets is referencing a null, Class Heal, Function: Use() Derived Class from base Class Ability. Object: " + gameObject.name);
          }
        }

  }
}
