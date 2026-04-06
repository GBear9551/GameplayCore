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

         var rb = target.GetComponent<Rigidbody>();

        if (rb != null)
        {

          // Get the force direction from the game object using this ability.
          // This needs to be stored in the unit, and the unit should have a force direction intention stored in it. 
          // Could come from a projectile if a projectile is running the ability as well. 
          var player = from.GetComponent<GameDevTV.FPS.Player>();

          if (player != null)
          {
            var forceDirection = player.GetForceDirectionFromShot();

            rb.AddForce(forceDirection * knockBackConfigSO.GetKnockBackForce());
          }
        }

         
      }



      return base.Use(from, targets);
    }

  }
}
