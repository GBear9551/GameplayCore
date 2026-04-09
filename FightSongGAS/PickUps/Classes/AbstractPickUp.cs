using FightSongGameLogicSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
  public abstract class AbstractPickUpBase<TUnit> : MonoBehaviour, IPickUp   where TUnit : Unit
  {
    [SerializeField] private AbilityRunner m_abilityRunner;

    private List<GameObject> m_targets = new();

    protected virtual void HandleTrigger(GameObject otherObject)
    {
      TUnit unit = otherObject.GetComponent<TUnit>();

      if (unit == null)
        return;

      Debug.Log("Trigger entered by: " + otherObject.name);

      m_targets.Add(otherObject);
      m_abilityRunner.UseAbility(m_targets);
      m_targets.Clear();

      Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
      HandleTrigger(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      HandleTrigger(other.gameObject);
    }
  }
}
