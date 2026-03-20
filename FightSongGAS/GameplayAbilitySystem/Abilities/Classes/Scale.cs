using UnityEngine;
using FightSongGameLogicSystem;
using System.Collections.Generic;


namespace FightSongGameLogicSystem
{
  public class Scale : AbstractAbility
  {

    [SerializeField] Vector3 m_BaseScalingAmount;


    public void Start()
    {
        bool isUsingCorrectAbilityConfig = AbilitySOConfigCheck<ScaleAbilityConfigSO>();       
    }

    public override List<IModifier> Use(GameObject from, List<GameObject> targets)
    {

      // Declare and initialize variables
      var scalingModifier = new ScaleModifier(m_BaseScalingAmount);

      // We must have a scaling modifier to apply the scaling ability.
      if (targets != null)
      {

        // Check to see if there is a scalingModifer
        if (scalingModifier != null)
        {

          // Set up who the modifier is from and who the targets of the modifier are.
          scalingModifier.SetFrom(from);
          scalingModifier.SetTargets(targets);

          // Check to see if the targets are units.
          foreach (GameObject target in targets)
          {
            var isUnit = target.GetComponent<Unit>();
            if (isUnit != null)
            {
              if (isUnit.m_Modifiers == null)
              {
                isUnit.m_Modifiers = new LinkedList<IModifier>();
              }

              // Stacking the same modifier upon cast. If you want the specific potion or scaling ability to not stack, check to see it is already contained in the list.
              // Faciliating stacking.
              isUnit.m_Modifiers.AddLast(scalingModifier);
            }

            // Cast Set scale
            isUnit.SetScale();

          }

        }


      }
      // Base class reporting
      return base.Use(from, targets);

    }
  }

}
