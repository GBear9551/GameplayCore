using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
    public abstract class Ability : MonoBehaviour, IAbility
    {


      [SerializeField] protected Effect OnCastEffect;

      

      public virtual void Use(GameObject from, List<GameObject> targets)
      {
        
         string targetsString = string.Empty;

         if(targets != null)
         {
           foreach(GameObject target in targets) 
           {
              targetsString += target.gameObject.name + " ";
           }
         }  

         Debug.Log("Ability: " + this.name + " Source of Ability: " + this.gameObject.name + " Ability targets: " + targetsString);
      }

     }
}
