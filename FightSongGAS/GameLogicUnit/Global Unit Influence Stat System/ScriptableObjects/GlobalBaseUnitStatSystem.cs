using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
  [CreateAssetMenu(fileName = "Game Base Stat System Configuration", menuName = "Scriptable Objects/BaseGameStatSystemConfiguration")]
  public class GlobalBaseUnitStatSystem : ScriptableObject
  {

     [SerializeField] int m_GlobalBaseMaxHealthAmount;
     [SerializeField] float m_GlobalBaseMovementSpeedAmount;

     
     public int GetBaseGameUnitMaxHealthAmount()
     {
        return m_GlobalBaseMaxHealthAmount;
     }

     public float GetBaseGameUnitMovementSpeedAmount() 
     {
       return m_GlobalBaseMovementSpeedAmount;
     }
  }

}
