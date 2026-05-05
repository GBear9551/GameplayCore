using FightSongGameLogicSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongTestBox
{
  public class TestAbilityBehaviour : MonoBehaviour, ITestAbilityBehaviour
  {

    [SerializeField] protected Color m_GizmoColor;
    [SerializeField] protected AbstractAbility m_Ability;

    void Awake()
    {
      if(m_Ability == null)
      {
        m_Ability = GetComponent<AbstractAbility>();
      }
    }

  }
}
