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
          MovementModifier movementSpeedBuff;

          // Cast ability configuration
          var abilityConfig = m_AbilityConfigSO as HasteAbilityConfigSO;

          // Create and initialize movement speed buff using the data from the game designer.
          movementSpeedBuff = new MovementModifier(abilityConfig.GetMovementSpeedModificationAmount());

          // Set up the movement speed buff/ or debuff if you make haste slow by passing a negative val for movementspeed via config.
           movementSpeedBuff.SetFrom(from);
           movementSpeedBuff.SetTargets(targets);

           foreach(GameObject target in targets)
           {
              var isUnit = target.GetComponent<Unit>();
              if(isUnit != null)
              {
                 if(isUnit.m_Modifiers == null)
                 {
                    isUnit.m_Modifiers = new LinkedList<IModifier>();
                 }

                 // Currently we are referencing a single memory space (m_MovementSpeedBuff), need more individualized memory spaces for each modifier added. 
                 // Stacking the same modifier upon cast. May need to check if the modifier is already in the list, if it is, then we can consider stacking it if stackable.
                 isUnit.m_Modifiers.AddLast(new MovementModifier(movementSpeedBuff));
              }
           }


             return base.Use(from, targets);
         }
    }
}
