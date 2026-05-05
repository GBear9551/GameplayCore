using FightSongGameLogicSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace FightSongTestBox
{
  public class AreaOfEffectTestViewAndUse : TestAbilityBehaviour
  {


    AreaOfEffectAbilityConfigSO m_AbilityConfiguration;

    private List<GameObject> m_Targets;

    private void Awake()
    {
      m_Targets = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
      if (m_AbilityConfiguration != null)
      {
        if (m_AbilityConfiguration.Is3D())
        {

        }
      }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if(m_AbilityConfiguration != null)
      {
         if(m_AbilityConfiguration.Is2D())
         {
           m_Targets.Add(other.gameObject);
           m_Ability.Use(this.gameObject, m_Targets);
           m_Targets.Clear();
         }
      }
    }

    private void Start()
    {
      // Get configuration data from the game designer
      m_AbilityConfiguration = m_Ability.GetAbilityConfig() as AreaOfEffectAbilityConfigSO;

      // Assume proper validation of config via Abstract Ability Awake() => config validation.
      

      // Create and attach sphere collider or circle collider to object based on 2d or 3d flag
      if(m_AbilityConfiguration != null )
      {
         if(m_AbilityConfiguration.Is2D())         
         {
            var circleCollider = this.AddComponent<CircleCollider2D>();
            circleCollider.enabled = true;
            circleCollider.isTrigger = true;
            circleCollider.radius = m_AbilityConfiguration.GetRadius();
         }
         else
         {

         }
      }

    }
 
    private void OnValidate()
    {
      if(m_AbilityConfiguration == null)
      {
        m_AbilityConfiguration = m_Ability.GetAbilityConfig() as AreaOfEffectAbilityConfigSO;
      }
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = m_GizmoColor;
      
      // Draw sphere
      
      Gizmos.DrawWireSphere(transform.position, m_AbilityConfiguration.GetRadius());

    }

    

  }
}
