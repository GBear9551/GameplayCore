using UnityEngine;


namespace FightSongGameLogicSystem
{
  [CreateAssetMenu(fileName = "DirectDamageAbilityConfigSO", menuName = "Scriptable Objects/AbilityConfigs/DamageConfig")]
  public class DirectDamageAbilityConfigSO : AbstractAbilityConfigSO
  {

    [SerializeField] protected float m_DirectDamageAmount;

    // Enum Type : Fire, Water ... etc

    public float GetDirectDamageAmount()
    {
       return m_DirectDamageAmount;
    }


  }
}
