using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using FightSongEventProgrammingSystem;
using FightSongLoggingSystem;
using System;





namespace FightSongGameLogicSystem
{
  public abstract class Unit : MonoBehaviour, IUnit
  {


    public LinkedList<IModifier> m_Modifiers = new LinkedList<IModifier>();
    public GameObject Target => null;
    [SerializeField] protected UnitConfig m_UnitConfig;

    // Programmer Data
    private GlobalBaseUnitStatSystem m_BaseUnitStatSystem;
    private int m_CurrentHealth;
    private Vector3 m_OriginialSize;


    // Game Designer Pluggable Events
    public UnityEvent<GameObject> OnDeath;

    // Need a place to subscribe to this event, thinking an event channel.
    //private UnitEventBus 


    // Locomotion State

  


    // Publishable events, programmer reporting 
    public event Action<OnTakeDamageEvent> OnTakeDamage;
    public event Action<OnHealEvent> OnHeal;
    public event Action<OnUnitSpawnEvent> OnUnitSpawn;
    
    // Event Objects, data containers, used to push to combat logs and achievement system. 
    private OnTakeDamageEvent m_UnitOnTakeDamageEvent;
    private OnUnitSpawnEvent m_UnitOnSpawnedEvent;
    private OnHealEvent m_UnitOnHealEvent;


    protected virtual void OnEnable()
    {
       Create();
    }

    public virtual void Create()
    {

      // Get the base game specific stat system. Used to make adjustments to every unit.
      m_BaseUnitStatSystem = m_UnitConfig.GetBaseStatSystem();
    

      // Set specific variables that are bound to the create event. Possibly pull data from other OnCreateResearch or Talents.
      m_CurrentHealth = m_UnitConfig.GetMaxHealthPointsAmount();
      m_OriginialSize = transform.localScale;

      // Handle On Create modifiers

      m_UnitOnSpawnedEvent = new OnUnitSpawnEvent()
      {
        MaxHealth = GetMaxHealth(),
        CurrentHealth = GetMaxHealth(),
        m_UnitDataModel = this
      };

      // Invoke the specific unit channel event, for unit specific extensions.
      OnUnitSpawn?.Invoke(m_UnitOnSpawnedEvent);

      // Report to combat logs, achievement system, and etc
       EventBus<OnUnitSpawnEvent>.Raise(m_UnitOnSpawnedEvent);

      // Compile logging when needed
      #if FIGHTSONG_LOGGING

        LoggingSystem.LogString($"[Combat] Unit Created: {this.gameObject.name}", Unity.VisualScripting.WarningLevel.Info);

      #endif 
       

    }

    public virtual int GetHealth()
    {
       return m_CurrentHealth;  
    }

    public virtual int GetMaxHealth()
    {
      int baseGameUnitMaxHealthModifier = m_BaseUnitStatSystem.GetBaseGameUnitMaxHealthAmount();
      return m_UnitConfig.GetMaxHealthPointsAmount() + baseGameUnitMaxHealthModifier;
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

      //Debug.Log("Total Speed: " + totalSpeed + "name: " + gameObject.name);
      return totalSpeed;
      
    }

    public virtual float GetJumpForce() 
    { 

       // Apply jump force modifiers.

       return m_UnitConfig.GetJumpForce();
    }

    public virtual float GetDoubleJumpForce()
    {
        // Apply double jump force modifiers.

        return m_UnitConfig.GetDoubleJumpForce();
    }

    public virtual int GetDamage() { return m_UnitConfig.GetBaseDamage(); }

    public virtual bool Die()
    {
      // Declare and initialize variables

      // Clear modifiers on death unless its ondeath modifier

      // Clearing all modifiers for current implementation
      m_Modifiers.Clear();


      // function stubb
      return true;
    }


    // Jump cooldown
    public virtual float GetJumpCooldown()
    {
        // Apply data modifiers to base jump cooldown data.

        // function stubb
        return m_UnitConfig.GetJumpInterval();
    }


    // Base functionality for a unit receiving a heal.
    public int Heal(int amountToHealBy, GameObject healingSource)
    {

      // Declare and initialize variables
      int totalHeal = 0;
      int newHealth = 0;
      int pastHealth = m_CurrentHealth;
      int amountHealed = 0;


      // Look at modifiers, does a heal modifier exist?
      if (m_Modifiers != null)
      {
        // Could be: var modifiers = m_Modifiers.OfType<HealModifier>();
        // and be a while loop for linked list traversal, but when the unit is healed on a time slice, all timed modifiers should be off or luckily be on here.
        foreach (HealModifier healModifier in m_Modifiers.OfType<HealModifier>())
        {
          totalHeal += healModifier.GetHealModifier();
        }
      }
      // TODO: Manage single use memory Property bag to remove single use modifiers? 

      // Constraints
      newHealth = totalHeal + amountToHealBy + m_CurrentHealth;

      if(m_CurrentHealth > 0)
      {

        if (newHealth > m_UnitConfig.GetMaxHealthPointsAmount())
        {
          m_CurrentHealth = m_UnitConfig.GetMaxHealthPointsAmount();
        }
        else
        {
          m_CurrentHealth = newHealth;
        }
      }

      amountHealed = m_CurrentHealth - pastHealth;  
      
      // Report healing done.
      Debug.Log("Target: " +  gameObject.name + " healed by this amount: " +  amountHealed);


      // Create heal event
      m_UnitOnHealEvent = new OnHealEvent()
      {
        HealthAfterHeal = m_CurrentHealth,
        m_UnitDataModel = this
      };

      // Invoke the specific unit channel event, for unit specific extensions.
      OnHeal?.Invoke(m_UnitOnHealEvent);

      // Report to combat logs, achievement system, and etc
      EventBus<OnHealEvent>.Raise(m_UnitOnHealEvent);


      #if FightSongLogging


      #endif



      // Report Sources of healing

      return amountHealed; 
    }

    // Base functionality for a unit receiving damage.
    public int TakeDamage(int baseDamageToApply, GameObject damageSource)
    {
       // Declare and initialize variables
        int totalDamage = 0;
        int modifiedDamage = 0;
        int pastHealth = m_CurrentHealth;
        int newHealth = 0;
        int damageDealt = 0;
        

      // If current health is zero don't take damage.
        if (m_CurrentHealth == 0) return 0;

      // If unit is invulnerable, take no damage, TODO: remove captured data, use calculation to avoid cache-invalidation.
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
        newHealth = m_CurrentHealth - totalDamage;

      // Constraints

      // Check to see if current health would drop below zero
      if(newHealth <= 0)
      {
          // Take Damage Effect

          // Check for prevent death modifiers

          // If no prevent death modifiers

          // Set the current health to zero.
            m_CurrentHealth = 0;

            // Function: Die() -> Care and think about deallocate or Destroy from here, should be Game Logic Specific.

              // Death Effect

              // Call Death Anim

              // Call OnDeath event 
 
      }

      // Else newHealth is not less than or equal to zero.
      else
      {  

        // Take Damage if damage was not zero
          m_CurrentHealth = newHealth;

        // Blocked Damage Effect
        // Check for block Modifiers in IModifiers

          // Zero Damage Effect
            //if(damageDealt == 0)
          

          // Took Damage Effect
         


      }

 
        // Calculate damage dealt
        damageDealt = pastHealth - m_CurrentHealth;

       

        // Reporting 
        m_UnitOnTakeDamageEvent = new OnTakeDamageEvent() {
                                            HealthAfterDamageTaken = m_CurrentHealth,
                                            m_UnitDataModel = this };


        // Invoke the specific unit channel event, for unit specific extensions.
        OnTakeDamage?.Invoke(m_UnitOnTakeDamageEvent);

        // Report to combat logs, achievement system, and etc                              
        EventBus<OnTakeDamageEvent>.Raise(m_UnitOnTakeDamageEvent);

        #if FIGHTSONG_COMBAT_LOG
          Debug.Log("Target: " +  gameObject.name + " damage dealt to this unit was: " +  damageDealt + " from: " + damageSource.name); 
        #endif

        return damageDealt;
    }

    // Base functionality for scaling a unit.
    public bool SetScale()
    {

       // Declare and initialize variables
       Vector3 originalScale = m_OriginialSize;
       Vector3 newScale = Vector3.zero;
       Vector3 maxSizeAllowed = m_UnitConfig.GetMaxSizeAllow();
       Vector3 minimumSizeAllowed = m_UnitConfig.GetMinimumSizeAllowed();
       Vector3 scalingAmount = Vector3.zero;

       // Compute scaling modifiers
        if (m_Modifiers != null)
        {
          foreach (ScaleModifier scaleModifier in m_Modifiers.OfType<ScaleModifier>())
          {
            scalingAmount += scaleModifier.GetModifier();
          }
        }

      // Apply computated scaling modifiers to newScale
        newScale = originalScale + scalingAmount;

      // Constraints, check max and min allowable size for the unit.  

      // Check for max size, probably should be component scale checking, magnitude might ease that for now.
      if ( (newScale.x > maxSizeAllowed.x)  &&
               (newScale.y > maxSizeAllowed.y) && 
                            (newScale.z > maxSizeAllowed.z) )
       {
          newScale = maxSizeAllowed;
       }

       else if(newScale.x < minimumSizeAllowed.x  && newScale.y < minimumSizeAllowed.y && newScale.z < minimumSizeAllowed.z) 
       {
          newScale = minimumSizeAllowed;
       }
      else
      {
        gameObject.transform.localScale = newScale;
      }

      // Logging
   
        return true;
    }

    private void Initialization()
    {
      m_CurrentHealth = m_UnitConfig.GetMaxHealthPointsAmount();
    }

    private void Start()
    {
      Initialization();
    }

    public virtual bool SetKnockBackDirection()
    {

      #if FIGHTSONG_LOGGING
        LoggingSystem.LogString($"[Combat - Ability - KnockBack] GameObject: {this.gameObject.name} KnockBackforce vector: <{GetKnockBackDirection()}> using default base class, please consider overriding in derived class.", Unity.VisualScripting.WarningLevel.Caution); 
      #endif

      return true;
    }

    public virtual Vector3 GetKnockBackDirection()
    {
      return transform.forward;
    }

  }
}
