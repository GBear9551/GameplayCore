using UnityEngine;



namespace FightSongGameLogicSystem
{

  public abstract class AbstractAbilityConfigSO : ScriptableObject
  {

    [SerializeField] protected AbilityType m_AbilityType;
    [SerializeField] protected float m_CastTime;
    [SerializeField] protected int m_CurrentAbilityLevel = 1;


  }

}
