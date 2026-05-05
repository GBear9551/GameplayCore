using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FightSongGameLogicSystem
{
  [CreateAssetMenu(fileName = "AreaOfEffectAbilityConfigSO", menuName = "Scriptable Objects/AbilityConfigs/AreaOfEffectConfig")]
  public class AreaOfEffectAbilityConfigSO : AbstractAbilityConfigSO
  {

    [Header("Area of Effect is defined by a sphere region")]
    [SerializeField] float m_Radius = 1f;

    [Header("2D: Use Vector3.forward (0f,0f,1f), if using 3D: Use Vector3.up (0f,1f,0f)")]
    [SerializeField] Vector3 m_DirectionFacing = Vector3.forward;

    [Header("Check this box if this area of effect ability is designed to effect friendly targets, such as the player.")]
    [SerializeField] bool m_EffectsFriendlyTargets = false;


    [Header("Number of Targets restriction: if this box is checked, then the area of effect will limit its ability to effect a certain number of targets.")]
    [SerializeField] bool m_LimitAreaOfEffectTargetSelection = false;
    [SerializeField] ushort m_MaxNumOfTargetsAllowedInSelection = 10;

    // Ability Runner Apply each ability to the targets gathers by the area of effect parameters
    [SerializeField] AbilityRunner abilityRunner; // Knock back isn't defined to be abstracted this way, I think. 


    public bool GetEffectsFriendlyFlag()
    { 
      return m_EffectsFriendlyTargets; 
    }

    public AbilityRunner GetAbilityRunner()
    { 
      return abilityRunner; 
    }

    public float GetRadius()
    {
      return m_Radius;
    }

    public bool Is2D()
    {
      if (m_DirectionFacing == Vector3.forward) return true;

      return false;
    }

    public bool Is3D()
    {
      if (m_DirectionFacing == Vector3.up) return true;
      return false;
    }

    public Vector3 GetAreaOfEffectSphereFacingDirection()
    {
      return m_DirectionFacing;
    }

  }
}
