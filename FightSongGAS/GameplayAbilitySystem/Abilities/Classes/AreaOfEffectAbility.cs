using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace FightSongGameLogicSystem
{

  public class AreaOfEffectAbility : AbstractAbility
  {

    private Collider[] m_AreaOfEffect3DTargets;
    private Collider2D[] m_AreaOfEffect2DTargets;

    //[SerializeField] GameObject m_TestObj;

    protected override bool ValidateConfig()
    {
      bool isUsingCorrectAbilityConfig = AbilitySOConfigCheck<AreaOfEffectAbilityConfigSO>();
      return isUsingCorrectAbilityConfig;
    }

    public override List<IModifier> Use(GameObject from, List<GameObject> targets)
    {

      // Get configuration data from the game designer
      var abilityConfiguration = m_AbilityConfigSO as AreaOfEffectAbilityConfigSO;
      var abilityRunner = abilityConfiguration.GetAbilityRunner();
      int index = 0;
      // Grab the deal direct damage modifiers from the the unit.
      var unitCastingContainsModifiersForAbilities = from.GetComponent<Unit>();

      // Area of Effect must generate a physics 2d or physics 3d, circle or sphere, to collect targets.
      // function: if targets null, generate physics overlap, and obtain targets.
      var projectile = from.GetComponent<Projectile>();
      if (projectile != null)
      {
        targets = HandleProjectileDelivery(from, abilityConfiguration);
      }

      // TODO: [Performance] Filter for units, be aware that aoe, can grab none units, use a layermask for optimization. 

      // Check friendly aoe flag, then make sure friendly targets are not effected.
      if(!abilityConfiguration.GetEffectsFriendlyFlag())
      {
         for(index = targets.Count-1;  index >= 0; index--)
         {

           var targetIsAUnit = targets[index].GetComponent<Unit>();

           if(targetIsAUnit != null)
           {
              if(targetIsAUnit.GetIsFriendly())
              {
                 targets.RemoveAt(index);
              }
           }
         }
      }

      // 

      // Pass filtered targets to ability runner.
      if (targets != null)
      {
        abilityRunner.SetFrom(from);
        abilityRunner.UseAbility(targets);
      }
        base.Use(from, targets);


      return null;
    }
 

    private List<GameObject> HandleProjectileDelivery(GameObject from, AreaOfEffectAbilityConfigSO configData)
    {
      // Declare and initialize variables
      Vector2 position = new Vector2(from.gameObject.transform.position.x, from.transform.position.y);
      float radius = configData.GetRadius();
      List<GameObject> list = new List<GameObject>();

      // Check to see if the area of effect is 2d, if yes, circle overlap it up baby.
      if(configData.Is2D())
      {
         m_AreaOfEffect2DTargets = Physics2D.OverlapCircleAll(position, radius);
         //Instantiate(m_TestObj,  position, Quaternion.identity);  
         foreach (var target in m_AreaOfEffect2DTargets)
         {
            var isUnit = target.GetComponent<Unit>();
            var isPlayer = target.GetComponent<PlayerGameFeelUnit>();

            if (isUnit != null && isPlayer == null)
            {
              list.Add(target.gameObject);
            }
         }
      }

      // else sphere overlap it up!
      else
      {
        m_AreaOfEffect3DTargets = Physics.OverlapSphere(transform.position, radius);
        
        foreach(var target in m_AreaOfEffect3DTargets)
        { 
           list.Add(target.gameObject);
        }
      }

      

        return list;
    }

  }
}
