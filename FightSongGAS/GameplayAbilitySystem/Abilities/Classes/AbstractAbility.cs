using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FightSongGameLogicSystem
{

    /*
     
      Bundles modifiers and sends them out based on the AbilityConfiguration Scriptable Object. 
    */

    public abstract class AbstractAbility : MonoBehaviour, IAbility
    {

      // Game Designer ( data )
      [SerializeField] protected AbstractAbilityConfigSO m_AbilityConfigSO;
      [SerializeField] protected Effect OnCastEffect;

        
        protected abstract bool ValidateConfig();


        protected static T AddTimedModifierComponent<T>(GameObject target) where T : TimedModifier
        {

           // Declare and initialize variables.

           // Get the timed modifier from the target game object.
           var compFromTarget  = target.GetComponent<T>();

           // Check to see if the component is on the target, if not, then add the timed modifier component of type T.
           if(compFromTarget == null )
           {
              compFromTarget = target.AddComponent<T>();
              return compFromTarget;
           }

           // Else the timed modifier component was not added, and was already present on the game object.
           return compFromTarget;
        }

        protected virtual void Awake()
        {
           ValidateConfig();
        }


        public virtual List<IModifier> Use(GameObject from, List<GameObject> targets)
        {
          string targetsString = string.Empty;

          if (OnCastEffect != null)
          {
            OnCastEffect.PlaySFX();
            OnCastEffect.PlayVFX();
          }

           if(targets != null)
           {
             foreach(GameObject target in targets) 
             {
                targetsString += target.gameObject.name + " ";
             }
           }  

           Debug.Log("Ability: " + this.name + " Source of Ability: " + this.gameObject.name + " Ability targets: " + targetsString);
           return null;
        }


        public bool AbilitySOConfigCheck<T>()
        {
 
          if(m_AbilityConfigSO != null) 
          {

             if(m_AbilityConfigSO is T)
             {
                return true;
             }
             else
             {

               Debug.LogError("This game object: " + this.gameObject.name + " must use a " + typeof(T).Name + ", not a: " + m_AbilityConfigSO.name);
               return false;
             }
              
          }
          
          Debug.LogError("This game object: " + this.gameObject.name + " is missing a ability configuration Scriptable Object instance.");
          return false;
        }
  }
}
