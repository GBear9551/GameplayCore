using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
    public class Haste : AbstractAbility
    {
 


       
       // Programmer ( data object ), move into function, possibly.
       MovementModifier m_MovementSpeedBuff;


    public void Start()
    {
        bool isUsingCorrectAbilityConfig = AbilitySOConfigCheck<HasteAbilityConfigSO>();       
    }

    public override List<IModifier> Use(GameObject from, List<GameObject> targets)
    {



        // Cast ability configuration
        var abilityConfig = m_AbilityConfigSO as HasteAbilityConfigSO;

        // Create and initialize movement speed buff using the data from the game designer.
        m_MovementSpeedBuff = new MovementModifier(abilityConfig.GetMovementSpeedModificationAmount());

        // If the constructor worked, then set up the modifier.
           if(m_MovementSpeedBuff != null)
           { 
             m_MovementSpeedBuff.SetFrom(from);
             m_MovementSpeedBuff.SetTargets(targets);

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
                   isUnit.m_Modifiers.AddLast(new MovementModifier(m_MovementSpeedBuff));
                }
             }

           }
           else
           {
              Debug.LogError("Movement Speed Buff is referencing a null, Class Haste, Function: Use() Derived Class from base Class Ability. Object: " + gameObject.name);
           }

           return base.Use(from, targets);
       }
    }
}
