using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem 
{
    public class Heal : AbstractAbility
    {

        protected override bool ValidateConfig()
        {
          bool isUsingCorrectAbilityConfig = AbilitySOConfigCheck<HealAbilityConfigSO>();
          return isUsingCorrectAbilityConfig;
        }

        public override List<IModifier> Use(GameObject from, List<GameObject> targets)
        {



          // Get ability configuration
          var healAbilityConfig = m_AbilityConfigSO as HealAbilityConfigSO;

          // Create a heal modifier to target the healing ability, who is healing who, 0 so it does not effect the overall heal.
          HealModifier healModifier = new HealModifier(0);

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
                  targetIsAUnit.m_Modifiers = new LinkedList<IModifier>();
                }

                     
                   
                    healModifier.SetFrom(from);
                    healModifier.SetTargets(targets);
                    targetIsAUnit.m_Modifiers.AddLast(new HealModifier(healModifier));
                  
                
                    targetIsAUnit.Heal( healAbilityConfig.GetHealAmount(), this.gameObject );

                    // Assuming single use healing and reporting from unit.heal is complete, remove single use heal modifiers or store in history.
                    targetIsAUnit.m_Modifiers.Remove(healModifier);
                   
                
              }
            }

          }
          else
          {
            Debug.LogError( " No targets to heal, targets is referencing a null, Class Heal, Function: Use() Derived Class from base Class Ability. Object: " + gameObject.name);
          }

          return base.Use(from, targets);

        }

  }
}
