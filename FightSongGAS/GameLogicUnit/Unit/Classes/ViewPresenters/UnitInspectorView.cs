using FightSongEventProgrammingSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace FightSongGameLogicSystem
{
    [RequireComponent(typeof(Unit))]
    public class UnitInspectorView : MonoBehaviour
    {

      [SerializeField] int m_UnitCurrentHealth;
      [SerializeField] int m_UnitMaxHealth;
      private Unit m_Unit;

      private void Awake()
      {
        m_Unit = GetComponent<Unit>();
      }



      public void OnEnable()
      {
         EventBus<OnTakeDamageEvent>.OnEvent += OnTakeDamage;
         EventBus<OnHealEvent>.OnEvent += OnHeal;
         EventBus<OnUnitSpawnEvent>.OnEvent += OnUnitCreatedEvent;
      }

      // Remember to unsubscribe i.e. pointer must be set to null at the end of object life time. 
      // Event Bus could handle this gracefully if programmed correctly, potentially
      public void OnDisable()
      {
        EventBus<OnTakeDamageEvent>.OnEvent -= OnTakeDamage;
        EventBus<OnHealEvent>.OnEvent -= OnHeal;
        EventBus<OnUnitSpawnEvent>.OnEvent -= OnUnitCreatedEvent;
      }
      public void OnHeal(OnHealEvent eventMessage)
      {
        if (m_Unit == eventMessage.m_UnitDataModel)
        {
          m_UnitCurrentHealth = m_Unit.GetHealth();
        }
      }
    private void OnUnitCreatedEvent(OnUnitSpawnEvent e)
      {
        if (m_Unit == e.m_UnitDataModel)
        {
          m_UnitCurrentHealth = e.CurrentHealth;
          m_UnitMaxHealth = e.MaxHealth;
        }
      }

      private void OnTakeDamage(OnTakeDamageEvent e)
      {
        if (m_Unit == e.m_UnitDataModel)
        {
          m_UnitCurrentHealth = e.HealthAfterDamageTaken;
        }
      }

      // Think like GameObject passing, but instead Event
      // Rememeber public UnityEvent<GameObject> OnSpawnEvent; 
      // Lead to functions of signature public void RandomizeColor(GameObject gameObject)

  }
}
