using UnityEngine;

namespace FightSongGameLogicSystem
{
    [CreateAssetMenu(fileName = "Unit Configuration", menuName = "Scriptable Objects/UnitConfiguration")]
    public class UnitConfig : ScriptableObject 
    {

      [Header("Game Designer Unit Data Model")]


      [Header("Game Specific Unit Base Modifier System - used to adjust 'all units' of a specific game")]
      [SerializeField] GlobalBaseUnitStatSystem m_BaseUnitStatSystem;

      [Header("Unit Movement")]
      [SerializeField] private float m_BaseGroundSpeed;
      [SerializeField] private float m_JumpBaseForce = 7f;
      [SerializeField] private float m_JumpBaseCooldown = 4f;
      [SerializeField] private float m_DoubleJumpBaseForce = 5f;


     [Header("Unit Combat")]
      [SerializeField] private int m_BaseDamage;
      [SerializeField] private float m_KnockBackForce;
      [SerializeField] private int m_MaxHealthPoints;

      [Header("Unit Allowable Size")]
      [SerializeField] private Vector3 m_MaxAllowedSize;
      [SerializeField] private Vector3 m_MinAllowedSize;




      [Header("NPC AI Brain")]
      [Tooltip("Changes the Unit's direction after this amount of time.")] 
      [SerializeField] private float m_ChangeDirectionInterval = 3f;



      public float GetDoubleJumpForce()
      {
         return m_DoubleJumpBaseForce;
      }

      public float GetChangeDirectionInterval()
      {
        return m_ChangeDirectionInterval; 
      }

      public float GetJumpForce()
      { 
        return m_JumpBaseForce; 
      }

      public float GetJumpInterval()
      {
         return m_JumpBaseCooldown;
      }

      public float GetKnockBackForce()
      {
        return m_KnockBackForce;
      }

      public int GetBaseDamage()
      {
        return m_BaseDamage;
      }

      public Vector3 GetMaxSizeAllow()
      {
        return m_MaxAllowedSize;
      }

      public Vector3 GetMinimumSizeAllowed()
      {
        return m_MinAllowedSize; 
      }

      public int GetMaxHealthPointsAmount()
      {
        return m_MaxHealthPoints; 
      }

      public float GetSpeed()
      {
        // To avoid a cached speed, we can compute the speed here everytime
        // and process foreach( IBuff where buff is IMovementModifier)
        return m_BaseGroundSpeed; 
      }

      public GlobalBaseUnitStatSystem GetBaseStatSystem()
      {
        return m_BaseUnitStatSystem;
      }

    }
}
