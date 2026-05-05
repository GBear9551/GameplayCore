using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
    public class Haste : AbstractAbility
    {


      

      protected override bool ValidateConfig()
      {
          bool isUsingCorrectAbilityConfig = AbilitySOConfigCheck<HasteAbilityConfigSO>();    
          return isUsingCorrectAbilityConfig;
      }

      public override List<IModifier> Use(GameObject from, List<GameObject> targets)
      {


          // Programmer ( data object ),
          IModifier speedModifier;

          // Cast ability configuration
          var hasteAbilityConfig = m_AbilityConfigSO as HasteAbilityConfigSO;
          bool isBuffTimed = hasteAbilityConfig.IsModifierTimed();
          float duration = hasteAbilityConfig.GetModifierDuration();
          float speedModificationAmount = hasteAbilityConfig.GetMovementSpeedModificationAmount();
          

          // Create and initialize movement speed buff using the data from the game designer.

           foreach(GameObject target in targets)
           {


              var isUnit = target.GetComponent<Unit>();

              if(isUnit != null) // => if(target.TryGetComponent<Unit>(out Unit unit))
              {
                 if(isUnit.m_Modifiers == null)
                 {
                    isUnit.m_Modifiers = new LinkedList<IModifier>();
                 }

                  // Add Timed Modifier if requested.
                  if(isBuffTimed)
                  { 
                    // Check to see if the timed modifier component for the movement speed is on the unit.

                    // Add the component if its not
                    // Add the haste timed modifier to the target if needed. DO THIS TO each target though
                     var timedSpeedModifier = AbstractAbility.AddTimedModifierComponent<MovementTimedModifier>(target);

                    // initialize the timed modifer
                     timedSpeedModifier.Initialize(from, targets, duration, speedModificationAmount);
               
                    // 

                    // Assign timedSpeedModifier, as the speedModifier, can either become timed or untimed.
                     speedModifier = timedSpeedModifier;

                  }

                  // Else add a non timed modifier
                  else
                  { 
                     speedModifier = new MovementModifier(speedModificationAmount);

                     // Set up the movement speed buff/ or debuff if you make haste slow by passing a negative val for movementspeed via config.
                     speedModifier.SetFrom(from);
                     speedModifier.SetTargets(targets);

                   }

                    // Currently we are referencing a single memory space (m_MovementSpeedBuff), need more individualized memory spaces for each modifier added. 
                    // Stacking the same modifier upon cast. May need to check if the modifier is already in the list, if it is, then we can consider stacking it if stackable.
                    // TODO is this different for timed vs not timed modifier, when considering adding the component and stacking modifiers?
                    //isUnit.m_Modifiers.AddLast(speedModifier);

                    // Prevents stacking invulnerability modifiers, but allows icons to stack. 
                    if (!isUnit.m_Modifiers.Contains(speedModifier))
                    {
                      isUnit.m_Modifiers.AddLast(speedModifier);
                    }


              }
           }


             return base.Use(from, targets);
         }
    }
}
