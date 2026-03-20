using UnityEngine;

namespace FightSongGameLogicSystem
{
  [CreateAssetMenu(fileName = "HasteAbilityConfigSO", menuName = "Scriptable Objects/AbilityConfigs/HasteConfig")]
  public class HasteAbilityConfigSO : AbstractAbilityConfigSO
  {

    [SerializeField] float m_MovementSpeedModificationAmount;

    // Amount to increase per level of the unit.

    // Talent/Research/Skill Tree stat adjustments

    public float GetMovementSpeedModificationAmount()
    { 
 
      return m_MovementSpeedModificationAmount; 

    }
  }
}
