using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace FightSongGameLogicSystem
{
  public abstract class Unit : MonoBehaviour, IUnit
  {
    public List<IModifier> m_Modifiers = new List<IModifier>();
    public GameObject Target => null;
    public UnitConfig m_UnitConfig;

    private int m_currentHealth;

    public UnityEvent<GameObject> OnDeath;


    public virtual int GetHealth()
    {
       return m_UnitConfig.GetMaxHealthPointsAmount();  
    }

    public virtual float GetSpeed()
    {
      float speedModifier = 0f;
      float totalSpeed = 0f;

      foreach(MovementModifier movement in m_Modifiers.OfType<MovementModifier>())
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

      // Declare and initialize variables
      int totalHeal = 0;
      int newHealth = 0;
      int pastHealth = m_currentHealth;
      int amountHealed = 0;

      // Look at modifiers, does a heal modifier exist?
      if (m_Modifiers != null)
      {
        foreach (HealModifier healModifier in m_Modifiers.OfType<HealModifier>())
        {
          totalHeal += healModifier.GetHealModifier();
        }
      }
      // TODO: Manage single use memory Property bag to remove single use modifiers? 

      // Constraints
      newHealth = totalHeal + amountToHealBy + m_currentHealth;

      if(m_currentHealth > 0)
      {

        if (newHealth > m_UnitConfig.GetMaxHealthPointsAmount())
        {
          m_currentHealth = m_UnitConfig.GetMaxHealthPointsAmount();
        }
        else
        {
          m_currentHealth = newHealth;
        }
      }

      amountHealed = m_currentHealth - pastHealth;  
      
      Debug.Log("Target: " +  gameObject.name + " healed by this amount: " +  amountHealed); 
      return amountHealed; 
    }

    // Base functionality for a unit receiving damage.
    public int TakeDamage(int baseDamageToApply, GameObject damageSource)
    {

       

       // Declare and initialize variables
        int totalDamage = 0;
        int modifiedDamage = 0;
        int pastHealth = m_currentHealth;
        int newHealth = 0;
        int damageDealt = 0;
        

      // If current health is zero don't take damage.
        if (m_currentHealth == 0) return 0;

      // If unit is invulnerable, take no damage
      bool cap = m_Modifiers.OfType<InvulnerabilityModifier>().Any();
      if (cap)
      {
        Debug.Log("Damage Dealt: 0 from " + damageSource.name + "because target: " + gameObject.name + " was invulnerable.");
        return 0;
      }

      // Handle Damage Modifiers
        if (m_Modifiers != null)
        {
          foreach (DamageModifier damageModifier in m_Modifiers.OfType<DamageModifier>())
          {
            modifiedDamage += damageModifier.GetDamageModifier();
          }
        }

      // Calculate total damage, new health, and damage dealt
        totalDamage = modifiedDamage + baseDamageToApply;
        newHealth = m_currentHealth - totalDamage;

      // Constraints

      // Check to see if current health would drop below zero
      if(newHealth <= 0)
      {
          // Take Damage Effect

          // Check for prevent death modifiers

          // If no prevent death modifiers

          // Set the current health to zero.
            m_currentHealth = 0;

            // Function: Die() -> Care and think about deallocate or Destroy from here, should be Game Logic Specific.

              // Death Effect

              // Call Death Anim

              // Call OnDeath event 
 
      }

      // Else newHealth is not less than or equal to zero.
      else
      {  

        // Take Damage if damage was not zero
          m_currentHealth = newHealth;

        // Blocked Damage Effect
        // Check for block Modifiers in IModifiers

          // Zero Damage Effect
            //if(damageDealt == 0)
          

          // Took Damage Effect
         


      }

 
        // Calculate damage dealt
        damageDealt = pastHealth - m_currentHealth;


        // Reporting 

        Debug.Log("Target: " +  gameObject.name + " damage dealt to this unit was: " +  damageDealt + " from: " + damageSource.name); 


        return damageDealt;
    }

    private void Initialization()
    {
      m_currentHealth = m_UnitConfig.GetMaxHealthPointsAmount();
    }

    private void Start()
    {
      Initialization();
    }
  }
}
