using UnityEngine;
using FightSongEventProgrammingSystem;

namespace FightSongGameLogicSystem 
{

    public class UnitHealthBarView : MonoBehaviour
    {

      // Unit Data Model
      [SerializeField] Unit m_Unit;

      private Vector3 m_OriginialScale;

      private void Awake()
      {
 
         m_OriginialScale = transform.localScale;

         if(m_Unit == null)
         {
             m_Unit = GetComponentInParent<Unit>();

             if(m_Unit == null)
             {
                m_Unit = GetComponentInChildren<Unit>();
             }

             if(m_Unit == null)
             {
                Debug.LogError("Class UnitHealthBarView, Function Awake(): No Unit Data Model attached to GameObject concept.");
             }

         }
      }

      public void OnTakeDamage(OnTakeDamageEvent eventMessage)
      {
         if(m_Unit == eventMessage.m_UnitDataModel)
         {
            UpdateHealthBar( m_Unit.GetHealth(), m_Unit.GetMaxHealth());
         }
      }

      public void OnHeal(OnHealEvent eventMessage)
      {
        if (m_Unit == eventMessage.m_UnitDataModel)
        {
          UpdateHealthBar(m_Unit.GetHealth(), m_Unit.GetMaxHealth());
        }
      }

      public void OnUnitCreated(OnUnitSpawnEvent eventMessage)
      {
        if (m_Unit == eventMessage.m_UnitDataModel)
        {
            UpdateHealthBar( m_Unit.GetHealth(), m_Unit.GetMaxHealth());
        }
      }


      public void UpdateHealthBar(float currentHealth, float maxHealth)
      {
         // Declare and initialize variables
         float healthBarSize = currentHealth / maxHealth;

         // Set the health bar to its new size.
         transform.localScale = new Vector3(healthBarSize * m_OriginialScale.x, m_OriginialScale.y, m_OriginialScale.z);
      }


      // On Enable Get the Unit's Current and Max Health
      public void OnEnable()
      {

        // Subscribe to unit on take damage event and on created
        EventBus<OnTakeDamageEvent>.OnEvent += OnTakeDamage;
        EventBus<OnHealEvent>.OnEvent += OnHeal;
        EventBus<OnUnitSpawnEvent>.OnEvent += OnUnitCreated;


      }


    // On Disable No longer subscribe to those events
      public void OnDisable()
      {

        EventBus<OnTakeDamageEvent>.OnEvent -= OnTakeDamage;
        EventBus<OnHealEvent>.OnEvent-= OnHeal;
        EventBus<OnUnitSpawnEvent>.OnEvent -= OnUnitCreated;

      }

    void LateUpdate()
    {
      transform.rotation = Quaternion.identity;
    }


  }
}
