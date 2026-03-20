using FightSongGameLogicSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Region : MonoBehaviour, IRegion
{

  // This can add multiple stacks of a modifier, it is the abilities responsibility to control stacking.
  // The region simply uses the abilities. if Contains(Modifier) -> dontadd(modifier) vs simply just adding(modifier)


  [SerializeField] AbilityRunner m_AbilityRunner;
  List<GameObject> m_Targets = new List<GameObject>();



  private void OnTriggerExit2D(Collider2D collision)
  {
    
     var unit = collision.gameObject.GetComponent<Unit>();

     if(unit != null )
     {

        // Get the modifiers stored on the unit
        var modifiers = unit.m_Modifiers;

        // Check to see if the modifiers are allocated.
        if(modifiers != null )
        {

          // Walk the linked list of modifiers.
          
          // Get the first modifier
          LinkedListNode<IModifier> currNode = modifiers.First;


          // All modifiers pushed by this region, must have this region as the from game object. 
          while( currNode != null ) 
          {

             var from = currNode.Value.GetFrom();

             if( from == this.gameObject)
             {
                // remove the modifier from the unit 
                modifiers.Remove( currNode );
                
             }

 
             // Get next node in the list
             currNode = currNode.Next;

          }
          // Look for the modifier from this region.
          //modifiers.OfType<T>


        }
     }

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    var unit = other.GetComponent<Unit>();

    if (unit != null)
    {
      Debug.Log("Trigger entered by: " + other.gameObject.name);
      m_Targets.Add(other.gameObject);
      m_AbilityRunner.UseAbility(m_Targets);
      m_Targets.Clear();
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    var unit = other.GetComponent<Unit>();

    if (unit != null)
    {
      Debug.Log("Trigger entered by: " + other.gameObject.name);
      m_Targets.Add(other.gameObject);
      m_AbilityRunner.UseAbility(m_Targets);
      //m_Modifiers = m_abilityRunner.UseAbility(m_targets);
      m_Targets.Clear();
    }
  }

}

