using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;
using System.Linq;

namespace FightSongGameLogicSystem
{
  public class TimedModifier : Modifier
  {
    protected float m_TotalDuration;
    public UnityEvent<List<GameObject>> OnModifierExpiredEvent;
    private float m_CurrDuration;
    private bool m_IsModifierActive;


    public virtual void Initialize()
    {
      m_CurrDuration = m_TotalDuration;
      m_IsModifierActive = true;
    }


    public bool SetModifierToActive()
    {
      m_IsModifierActive = true;
      return true;
    }

    public bool SetTotalDuration(float duration)
    {
      m_TotalDuration = duration;
      return true;
    }

    // Update is called once per frame
    void Update()
    {

      if (m_IsModifierActive)
      {
        m_CurrDuration -= Time.deltaTime;

        if (m_CurrDuration <= 0f)
        {
          m_IsModifierActive = false;

          Debug.Log("Removing: " + this + "because timed modifier expired. GOName: " + gameObject.name);

          // Remove timedModifier
          if(Remove() == false)
          {
            Debug.LogError("Attempted to remove a modifier that was not registered to a unit. Class TimedModifier(), Func: Update()");
          }

          if (m_Targets != null)
          {
            OnModifierExpiredEvent?.Invoke(m_Targets);
          }
        }
      }

    }
    

    public override bool Remove()
    {
      if (m_Targets != null)
      {
        foreach (GameObject target in m_Targets)
        {
          var IsTargetAUnit = target.GetComponent<Unit>();
          if (IsTargetAUnit != null)
          {
            if (IsTargetAUnit.m_Modifiers != null)
            {
              bool wasRemoved = IsTargetAUnit.m_Modifiers.Remove(this);
              return wasRemoved;
            }
          }
        }
      }
      return false;
    }
  }
}
