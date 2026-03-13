using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace FightSongGameLogicSystem
{
  public abstract class Modifier: MonoBehaviour, IModifier
  {

    GameObject m_From;
    List<GameObject> m_Targets;

    // async remove modifier from targets after delay

    public virtual bool SetFrom(GameObject from)
    {

       if (from == null)
       { 
          Debug.LogError("A buff must be delievered from some gameobject. Base Class SetFrom()" + gameObject.name);
          return false;
       }

       m_From = from;
       return true;
    }

    public virtual bool SetTargets(List<GameObject> targets)
    {

      if (targets == null)
      {
        Debug.LogError("A buff must be delievered from some gameobject. Base Class SetFrom()" + gameObject.name);
        return false;
      }

      m_Targets = targets;
      return true;
    }

    public virtual void Apply()
    {
      Debug.Log("Derived class did not implement base class Buff function Apply() from " + gameObject.name);
    }

    public virtual void Remove()
    {

      Debug.Log("Derived class did not implement base class Buff function Remove() from " + gameObject.name);
    }
  }
}
