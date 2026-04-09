using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FightSongGameLogicSystem
{
  [CreateAssetMenu(fileName = "2D Platformer Movement Configuration", menuName = "Scriptable Objects/PlatformerMovementConfiguration")]
  public class PlatformerMovement2DConfigSO : ScriptableObject
  {
     [Header("Gravity Mechanics")]
     [SerializeField] private float m_ExtraGravity = 20f;

     [Tooltip("Extra gravity will occur after this amount of time spent in air.")]
     [SerializeField] private float m_ExtraGravityDelay = 0.2f;

    [Header("Coyote Time: A jump can be used during the time given to unit after moving off an edge. 'Wiggle Room to jump'")]
    [SerializeField] private float m_CoyoteTime = 0.5f;


     public float GetCoyoteTime()
     { 
       return m_CoyoteTime; 
     }

     public float GetExtraGravity()
     { 
       return m_ExtraGravity; 
     }

     public float GetExtraGravityDelay()
     {
       return m_ExtraGravityDelay;
     }

  }

}
