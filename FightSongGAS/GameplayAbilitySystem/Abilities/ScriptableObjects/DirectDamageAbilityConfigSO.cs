using UnityEngine;


namespace FightSongGameLogicSystem
{
  [CreateAssetMenu(fileName = "DirectDamageAbilityConfigSO", menuName = "Scriptable Objects/AbilityConfigs/DamageConfig")]
  public class DirectDamageAbilityConfigSO : AbstractAbilityConfigSO
  {

    [SerializeField] protected int m_DirectDamageAmount;

    // Enum Type : Fire, Water ... etc

    public int GetDirectDamageAmount()
    {
       return m_DirectDamageAmount;
    }


  }
}
