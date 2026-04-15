using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
  public class KnockBack : AbstractAbility
  {

    protected override bool ValidateConfig()
    {
      bool isUsingCorrectAbilityConfig = AbilitySOConfigCheck<KnockBackConfigSO>();
      return isUsingCorrectAbilityConfig;
    }

    public override List<IModifier> Use(GameObject from, List<GameObject> targets)
    {


      // Consider controlling the number of knockbacks that can be applied to an object in a given moment here via a knockback modifier control system.
      KnockBackConfigSO knockBackConfigSO = m_AbilityConfigSO as KnockBackConfigSO;

      // Loop through the targets, if the targets have rigidbodies, then they may be knocked back.
      foreach(GameObject target in targets) 
      {

         // Check for knockback prevention modifiers. If any exist on the unit(target) then do not knock it back. 

         //var rb = target.GetComponent<Rigidbody>();
         var rb2D = target.GetComponent<Rigidbody2D>();
         var targetAsUnit = target.GetComponent<Unit>();

        if (rb2D != null)
        {

          // Get the force direction from the game object using this ability.
          // This needs to be stored in the unit, and the unit should have a force direction intention stored in it. 
          // Could come from a projectile if a projectile is running the ability as well. 
          var player = from.GetComponent<Unit>();
          var projectile = from.GetComponent<Projectile>();

          if (player != null)
          {

            var forceDirection = player.GetKnockBackDirection();

            rb2D.AddForce(forceDirection * knockBackConfigSO.GetKnockBackForce(), ForceMode2D.Impulse);
          }
          else if(projectile != null)
          {
            var forceDirection = projectile.GetKnockBackDirection();

            rb2D.AddForce(forceDirection * knockBackConfigSO.GetKnockBackForce(), ForceMode2D.Impulse);


            // Add a modifier to cancel stun the speed of the target for the knockback duration.
            var knockBackModifier = AbstractAbility.AddTimedModifierComponent<KnockBackModifier>(target);
           
            // 
            if (targetAsUnit.m_Modifiers == null)
            {
              targetAsUnit.m_Modifiers = new LinkedList<IModifier>();
            }

            knockBackModifier.Initialize(from, targets, knockBackConfigSO.GetKnockBackStunDuration());

            // Prevents stacking invulnerability modifiers, but allows icons to stack. 
            if (!targetAsUnit.m_Modifiers.Contains(knockBackModifier))
            {
              targetAsUnit.m_Modifiers.AddLast(knockBackModifier);
            }


          }


        }

         
      }



      return base.Use(from, targets);
    }

  }
}
