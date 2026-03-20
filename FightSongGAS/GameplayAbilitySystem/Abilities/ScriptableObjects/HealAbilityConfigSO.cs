using UnityEngine;


namespace FightSongGameLogicSystem
{

  [CreateAssetMenu(fileName = "HealAbilityConfigSO", menuName = "Scriptable Objects/AbilityConfigs/HealConfig")]
  public class HealAbilityConfigSO : AbstractAbilityConfigSO
  {
     [SerializeField] protected int m_HealAmount;

     public int GetHealAmount()
     {

      return m_HealAmount; 

     }
  }
}
