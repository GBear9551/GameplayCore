using UnityEngine;

namespace FightSongGameLogicSystem
{


  [CreateAssetMenu(fileName = "KnockBackAbilityConfigSO", menuName = "Scriptable Objects/AbilityConfigs/KnockBackConfig")]
  public class KnockBackConfigSO : AbstractAbilityConfigSO 
    {

        [SerializeField] float m_KnockBackForce = 1f;
        //[SerializeField] new protected bool m_TimedBuff = false;
        //[SerializeField] new protected float m_BuffDuration = 0f;

    public float GetKnockBackForce() {  return m_KnockBackForce; }
 
    }
}
