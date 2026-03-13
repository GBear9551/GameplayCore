using UnityEngine;

namespace FightSongGameLogicSystem
{
    [CreateAssetMenu(fileName = "Unit Configuration", menuName = "Scriptable Objects/UnitConfiguration")]
    public class UnitConfig : ScriptableObject 
    {

      [SerializeField] private float m_baseGroundSpeed;
      [SerializeField] private int m_MaxHealthPoints;


      public int GetMaxHealthPointsAmount()
      {
        return m_MaxHealthPoints; 
      }

      public float GetSpeed()
      {
        // To avoid a cached speed, we can compute the speed here everytime
        // and process foreach( IBuff where buff is IMovementModifier)
        return m_baseGroundSpeed; 
      }

    }
}
