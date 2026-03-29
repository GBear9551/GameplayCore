using UnityEngine;

namespace FightSongGameLogicSystem
{
    [CreateAssetMenu(fileName = "Unit Configuration", menuName = "Scriptable Objects/UnitConfiguration")]
    public class UnitConfig : ScriptableObject 
    {

      [SerializeField] private float m_BaseGroundSpeed;
      [SerializeField] private int m_MaxHealthPoints;
      [SerializeField] private Vector3 m_MaxAllowedSize;
      [SerializeField] private Vector3 m_MinAllowedSize;


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

    }
}
