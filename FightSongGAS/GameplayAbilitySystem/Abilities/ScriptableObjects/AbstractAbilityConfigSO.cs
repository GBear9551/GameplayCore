using UnityEngine;



namespace FightSongGameLogicSystem
{

  public abstract class AbstractAbilityConfigSO : ScriptableObject
  {

    [SerializeField] protected AbilityType m_AbilityType;
    [SerializeField] protected float m_CastTime;
    [SerializeField] protected int m_CurrentAbilityLevel = 1;
    [Header("Please check this box if you want the buff to last a specific amount of time, else it will be premanent")]
    [SerializeField] bool m_TimedBuff = true;

    [Tooltip("Buff Duration, this buff will be removed after this amount of time in seconds.")]
    [SerializeField] float m_BuffDuration = 1f;

    public bool IsModifierTimed()
    {
       return m_TimedBuff;
    }

    public float GetModifierDuration()
    { 
 
      return m_BuffDuration; 

    }

  }

}
