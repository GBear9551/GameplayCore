using Cinemachine;
using FightSongLoggingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace FightSongGameLogicSystem
{
  public class Projectile : MonoBehaviour, IProjectile
  {

    // Game Designer Data
    [Header("Creating an ability runner can be fun! Attach it to the projectile config SO")]
    [SerializeField] ProjectileSO m_ProjectileConfigSO;
    [SerializeField] Effect m_OnHitEffect;
    [SerializeField] Effect m_OnCreateEffect;


    // Projectile Modifiers
    public LinkedList<MovementModifier> m_MovementModifiers;

    // Programmer Data
    private List<GameObject> m_targets = new();
    private Rigidbody m_Rigidbody3D;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_directionToTravel;
    private PooledGameObject m_PooledObject;


    private void OnEnable()
    {
      m_MovementModifiers = new LinkedList<MovementModifier>();
      
      if(m_OnCreateEffect != null)
      {
        m_OnCreateEffect.PlaySFX();
      }

    }

    private void OnDisable()
    {
      m_MovementModifiers.Clear();
      m_MovementModifiers = new LinkedList<MovementModifier>();
    }

    public void Initialize(Vector3 directionToTravel)
    {
       m_directionToTravel = directionToTravel;
    }

    public Vector3 GetKnockBackDirection()
    {
      return m_directionToTravel;
    }

    void Start()
    {

      m_Rigidbody3D = GetComponent<Rigidbody>();
      m_Rigidbody2D = GetComponent<Rigidbody2D>();
      m_PooledObject = GetComponent<PooledGameObject>();
      if (m_Rigidbody2D == null && m_Rigidbody3D == null)
      {
        LoggingSystem.LogString("[DEBUG - ERROR] Class: Projectile.cs Function FixedUpdate() Missing rigidbody component", Unity.VisualScripting.WarningLevel.Info);
      }
    }

    // Projectile Motion
    void FixedUpdate()
    {
      if (m_Rigidbody2D != null)
      {
        m_Rigidbody2D.velocity = m_directionToTravel * GetSpeed(); 
      }

      else if (m_Rigidbody3D != null)
      {

      }

    }


    public float GetSpeed()
    {
      float baseMovementSpeed = m_ProjectileConfigSO.GetMovementSpeed();
      float modifiedSpeed = 0f; 
      foreach(var node in m_MovementModifiers) 
      {
          modifiedSpeed += node.GetMovementModifier();
      }

      return modifiedSpeed + baseMovementSpeed;

    }


    // Game Logic
    protected virtual void HandleTrigger(GameObject otherObject)
    {

      if (otherObject.layer == LayerMask.NameToLayer("TestLayer")) return;
      PlayOnHitEffect();

      Unit unit = otherObject.GetComponent<Unit>();

      // Consider friendly fire


      if (unit != null && otherObject.layer != LayerMask.NameToLayer("Friendly")) 
      {

        Debug.Log("Trigger entered by: " + otherObject.name);

        m_targets.Add(otherObject);
        var abilityRunner = m_ProjectileConfigSO.GetAbilityRunner();

        if (abilityRunner != null)
        {
          abilityRunner.SetFrom(this.gameObject);
          abilityRunner.UseAbility(m_targets);
        }
        m_targets.Clear();


       // This is happening twice [BUG] TODO
      // If Projectile is not piercing 
       m_PooledObject.ReturnToPool();

      }

      if (otherObject.layer == LayerMask.NameToLayer("Ground"))
      {
        // If Projectile is not piercing 
        m_PooledObject.ReturnToPool();
      }

      else
      {
        m_PooledObject.ReturnToPool();
      }
       
    }
    protected virtual void PlayOnHitEffect()
    {
      if (m_OnHitEffect != null)
      {
        m_OnHitEffect.PlayVFX();
      }
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
