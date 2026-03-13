using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace FightSongGameLogicSystem 
{
    public class DealDirectDamage : Ability
    {

       [SerializeField] int m_DamageAmount;
       [SerializeField] List<DamageModifier> m_DamageModifiers;

      public override void Use(GameObject from, List<GameObject> targets)
      {
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
                targetIsAUnit.m_Modifiers = new List<IModifier>();
              }

              if (m_DamageModifiers != null)
              {
                foreach (DamageModifier damageModifier in m_DamageModifiers)
                {
                  damageModifier.SetFrom(from);
                  damageModifier.SetTargets(targets);
                  targetIsAUnit.m_Modifiers.Add(damageModifier);
                }
              }

              targetIsAUnit.TakeDamage(m_DamageAmount, this.gameObject);


            }
          }

        }
        else
        {
          Debug.LogError(" No targets to heal, targets is referencing a null, Class Heal, Function: Use() Derived Class from base Class Ability. Object: " + gameObject.name);
        }
      }



  }
}
