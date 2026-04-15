using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using FightSongLoggingSystem;

namespace FightSongGameLogicSystem 
{
    public class DealDirectDamage : AbstractAbility
    {

      protected override bool ValidateConfig()
      {
        bool isUsingCorrectAbilityConfig = AbilitySOConfigCheck<DirectDamageAbilityConfigSO>();
        return isUsingCorrectAbilityConfig;
      }

      public override List<IModifier> Use(GameObject from, List<GameObject> targets)
      {

        // Get configuration data from the game designer
        var abilityConfiguration = m_AbilityConfigSO as DirectDamageAbilityConfigSO;

        // Grab the deal direct damage modifiers from the the unit.
        var unitCastingDealDirectDamage = from.GetComponent<Unit>();

        if(unitCastingDealDirectDamage == null)
        {
          LoggingSystem.LogString("[DEBUG COMBAT] A none unit is casting an ability, please correct the 'from' data member, class DealDirectDamage, function Use(...)", Unity.VisualScripting.WarningLevel.Info);
        }

        // Create the direct damage modifier for history and reporting.
        DamageModifier directDamageModifier = new DamageModifier(0);

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

                  directDamageModifier.SetFrom(from);
                  directDamageModifier.SetTargets(targets);
                  targetIsAUnit.m_Modifiers.AddLast(new DamageModifier(directDamageModifier));
                  targetIsAUnit.TakeDamage(abilityConfiguration.GetDirectDamageAmount(), this.gameObject);

            }
          }

        }
        else
        {
          Debug.LogError(" No targets to heal, targets is referencing a null, Class Heal, Function: Use() Derived Class from base Class Ability. Object: " + gameObject.name);
        }

        return base.Use(from, targets);

      }



  }
}
