using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace FightSongGameLogicSystem
{
    public class PickUp : MonoBehaviour, IPickUp
    {

      [SerializeField] AbilityRunner m_abilityRunner;
      List<GameObject> m_targets = new List<GameObject>();
      
      private void OnTriggerEnter(Collider other)
      { 
         var unit = other.GetComponent<Unit>();
         
         if (unit != null) 
         {
           Debug.Log("Trigger entered by: " + other.gameObject.name);
           m_targets.Add(other.gameObject);
           m_abilityRunner.UseAbility(m_targets);
           m_targets.Clear();
         }
         //Destroy(this.gameObject);
      }

  }
}
