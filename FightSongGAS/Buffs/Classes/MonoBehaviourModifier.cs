using FightSongGameLogicSystem;
using System.Collections.Generic;
using UnityEngine;


public class MonoBehaviourModifier : MonoBehaviour, IModifier
{
  protected GameObject m_From;
  protected List<GameObject> m_Targets;

  // async remove modifier from targets after delay
  public GameObject GetFrom()
  {
    return m_From;
  }

  public virtual bool SetFrom(GameObject from)
  {

    if (from == null)
    {
      Debug.LogError("A buff must be delievered from some gameobject. Base Class SetFrom()" + from.name);
      return false;
    }

    m_From = from;
    return true;
  }

  public virtual bool SetTargets(List<GameObject> targets)
  {

    if (targets == null)
    {
      Debug.LogError("A buff must be delievered from some gameobject. Base Class SetFrom()" + m_From.name);
      return false;
    }

    m_Targets = new List<GameObject>(targets);
    return true;
  }

  public virtual bool Apply()
  {
    Debug.Log("Derived class did not implement base class Modifier function Apply() from " + m_From.name);
    return false;
  }

  public virtual bool Remove()
  {

    // Declare and initialize variables.

    // If we have valid targets
    if (m_Targets != null)
    {

      // Check each target
      foreach (GameObject target in m_Targets)
      {

        // if that target is a unit
        if (target.TryGetComponent<Unit>(out var unit))
        {

          // Check to see if the unit has a valid modifer list
          if (unit.m_Modifiers != null)
          {
            // Remove this current modifier from the list of modifiers.
            unit.m_Modifiers.Remove(this);
          }

        }

      }

    }


    Debug.Log("Derived class did not implement base class Modifier function Remove() from " + m_From.name);
    return false;
  }

  public virtual bool Refresh()
  {
    Debug.Log("Derived class did not implement base class Modifier function Refresh() from " + m_From.name);
    return false;
  }
}

