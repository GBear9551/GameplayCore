using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;
using System.Linq;
using FightSongLoggingSystem;

namespace FightSongGameLogicSystem
{
  public class TimedModifier : MonoBehaviourModifier
  {
    protected float m_TotalDuration;
    public UnityEvent<List<GameObject>> OnModifierExpiredEvent;
    protected float m_CurrDuration;




    private void OnDisable()
    {
      Remove();
      SetModifierToInactive();
    }

    public override void Initialize(GameObject from, List<GameObject> targets, float duration)
    {

      // Set the timed modifier's duration
      m_TotalDuration = duration;
      m_CurrDuration = m_TotalDuration;

      // Set from
      SetFrom(from);

      // Set targets for the modifier
      SetTargets(targets);      

      Debug.Log(this.name + this.GetInstanceID().ToString() + " from: " + m_From + "has " + GetCurrentDurationRemaining() + "time remaining.");
      m_CurrDuration = m_TotalDuration;
      SetModifierToActive();
    }



    // Update is called once per frame
    protected virtual void Update()
    {

      if (m_IsModifierActive)
      {
        m_CurrDuration -= Time.deltaTime;

        if (m_CurrDuration <= 0f )
        {
          m_IsModifierActive = false;

          Debug.Log("Removing: " + this + "because timed modifier expired. GOName: " + gameObject.name);
          // Remove timedModifier
          if(Remove() == false)
          {
            // If this message is received at console, check the objects OnDisable() and handle TimedModifiers,
            var obj = this;
            Debug.LogError("Attempted to remove a modifier that was not registered to a unit. Class TimedModifier(), Func: Update()");
          }
          /*foreach(var target in m_Targets)
          {
            var unit = target.GetComponent<Unit>();
            if(unit != null)
            {
              var timedModifiers = unit.m_Modifiers.OfType<TimedModifier>();
              if(timedModifiers != null)
              {
                Debug.Log("The unit: " + unit.gameObject.name + " has this many timed modifiers on it: " + timedModifiers.Count().ToString());
              }
            }
          }*/

          if (m_Targets != null)
          {
            OnModifierExpiredEvent?.Invoke(m_Targets);
          }
        }
      }

    }

    private bool SetTotalDuration(float duration)
    {
      m_TotalDuration = duration;
      return true;
    }

    public float GetCurrentDurationRemaining()
    { 
      return m_CurrDuration; 
    }

    public override bool Remove()
    {
      if(m_Targets == null)
        return false;

      if (m_Targets != null)
      {
        // Bad, TODO: Supplying unnecessary target information is causing unnecessary looping across targets during removal.
        //foreach (GameObject target in m_Targets)
        //{
          var IsTargetAUnit = GetComponent<Unit>();
          if (IsTargetAUnit != null)
          {
            if (IsTargetAUnit.m_Modifiers != null)
            {
              bool wasRemoved = IsTargetAUnit.m_Modifiers.Remove(this);
              LoggingSystem.LogString("[DEBUG] Class Timed Modifer: Function Remove(): [Info] Target Unit: " + IsTargetAUnit.name + ": " + wasRemoved, Unity.VisualScripting.WarningLevel.Info);
              //return wasRemoved;
            }
          }
        //}
      }
      m_Targets.Clear();
      return true;
    }
  }
}
