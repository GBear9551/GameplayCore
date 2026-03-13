using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace FightSongGameLogicSystem
{
  public abstract class Unit : MonoBehaviour, IUnit
  {
    public List<IModifier> m_Modifiers = new List<IModifier>();
    public GameObject Target => null;
    public UnitConfig m_UnitConfig;


    public virtual float GetSpeed()
    {
      float speedModifier = 0f;
      float totalSpeed = 0f;

      foreach(MovementModifier movement in m_Modifiers) 
      {
          speedModifier += movement.GetMovementModifier();
      }

      

      totalSpeed = m_UnitConfig.GetSpeed() + speedModifier;
      

      if(totalSpeed < 0f)
      {
        totalSpeed = 0f;
      }

      Debug.Log("Total Speed: " + totalSpeed + "name: " + gameObject.name);
      return totalSpeed;
      
    }


    // Base functionality for a unit receiving a heal.
    public int Heal(int amountToHealBy, GameObject healingSource)
    {
      return 0; 
    }

    // Base functionality for a unit receiving damage.
    public int TakeDamage(int damageToApply, GameObject damageSource)
    {
      return 0;
    }
  }
}
