using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
    public abstract class Ability : MonoBehaviour, IAbility
    {
      public virtual void Use(GameObject gameObj, List<GameObject> targets)
      {
        Debug.Log("Derived class doesn't override abstract class Ability Function: Use(GameObject gamObj, List<ITargetable> targets) from: " + gameObject.name);
      }

     }
}
