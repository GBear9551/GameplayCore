using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{

  [CreateAssetMenu(fileName = "Projectile Configuration", menuName = "Scriptable Objects/ProjectileConfiguration")]
  public class ProjectileSO : ScriptableObject
  {

    [SerializeField] float m_MovementSpeed;
    [SerializeField] AbilityRunner m_AbilityRunner;


    public AbilityRunner GetAbilityRunner()
    {
      return m_AbilityRunner;
    }

    public float GetMovementSpeed()
    {
       return m_MovementSpeed;
    }


  }
}
