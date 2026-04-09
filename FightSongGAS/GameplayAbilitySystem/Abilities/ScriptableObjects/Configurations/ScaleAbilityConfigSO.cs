using UnityEngine;
namespace FightSongGameLogicSystem
{

  [CreateAssetMenu(fileName = "ScaleAbilityConfigSO", menuName = "Scriptable Objects/AbilityConfigs/ScaleConfig")]
  public class ScaleAbilityConfigSO : AbstractAbilityConfigSO
  {

    [SerializeField] Vector3 m_ScaleAmount;
 
    public Vector3 GetScaleAmount() 
    { 

      return m_ScaleAmount; 

    }

  }
}
