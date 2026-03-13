using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
    public class Haste : Ability
    {
 
       [SerializeField] MovementModifier m_MovementSpeedBuff;
        
       public override void Use(GameObject from, List<GameObject> targets)
       {
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
                      isUnit.m_Modifiers = new List<IModifier>();
                   }

                   isUnit.m_Modifiers.Add(m_MovementSpeedBuff);
                }
             }

           }
           else
           {
              Debug.LogError("Movement Speed Buff is referencing a null, Class Haste, Function: Use() Derived Class from base Class Ability. Object: " + gameObject.name);
           }
       }
    }
}
