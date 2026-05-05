using FightSongLoggingSystem;
using FightSongStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace FightSongGameLogicSystem
{
  [RequireComponent(typeof(Rigidbody2D),typeof(Unit),typeof(LocomotionStateMachine))]
  public class Unit2DPlatformerMovement : MonoBehaviour, I2DUnitPlatformerMovement
  {


    protected Unit m_Unit; // Contains unit's speed modifiers(like haste and slow pickups)

    [SerializeField] protected PlatformerMovement2DConfigSO m_PlatformerMovement2DConfigSO; 
    [SerializeField] private Transform m_FeetTransform;
    [SerializeField] private Vector2 m_FootSize;
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private float m_BigLandingSpeedThreshold;

    protected bool m_IsGrounded = false;
    protected Rigidbody2D m_RigidBody2D;
    protected bool m_DoubleJumpAvailable = true;
    protected float m_CoyoteTimer;
    protected LocomotionStateMachine m_LocomotionStateMachine;
    protected float m_LandingForce;

    private JetpackMechanics m_JetpackMechanics;
    private Vector2 m_MovementVector;
    private float m_TimeSpentInAir;
    private float m_OriginalGravityScale;
    

    public event Action OnBigLandEvent;
    private InputFrame m_InputFrame;


    private float m_PreviousVelocity_Y_Speed = 0f;
    private float m_CurrVel_Y_Speed = 0f;
    private bool m_IsExtraGravityAllowed = false;


    private void OnDrawGizmos()
    {
      Gizmos.color = Color.red;

      Gizmos.DrawWireCube(m_FeetTransform.position, m_FootSize);
    }

    public bool SetMovementVector(Vector2 movementVector)
    {

      return true;

    }

    protected virtual void Awake()
    {

      m_LocomotionStateMachine = GetComponent<LocomotionStateMachine>();  
      m_RigidBody2D = GetComponent<Rigidbody2D>();
      m_JetpackMechanics = GetComponent<JetpackMechanics>();
      m_OriginalGravityScale = m_RigidBody2D.gravityScale;
      m_CoyoteTimer = m_PlatformerMovement2DConfigSO.GetCoyoteTime();
  

      m_Unit = GetComponent<Unit>();  
 
      if(m_FeetTransform == null)
      {
        LoggingSystem.LogString("[DEBUG] Class Unit2DPlatformerMovement function Awake(), missing feet transform!", Unity.VisualScripting.WarningLevel.Error);
      }

    }

    protected virtual void Update()
    {

      // Pass input frame into movement and jump function, at fixedUpdate
        m_IsGrounded = CheckGrounded();

      // If we are not grounded then track time spent in air.
      if(!m_IsGrounded)
      {
        m_TimeSpentInAir += Time.deltaTime;
        m_LocomotionStateMachine.Fall();
      }
      else
      {
        m_TimeSpentInAir = 0f;
        m_DoubleJumpAvailable = true;
        
        // The overlapping box will be present more often when the frame rate is higher
        // Handle the landing with velocity to ensure the character isn't heading upward with the overlapping box still being grounded.
        if (!(m_RigidBody2D.velocity.y > 0f))
        {
          m_LocomotionStateMachine.Land();
        }
      }

      // Handle Coyote time
      HandleCoyoteTime();

    }



    protected virtual void FixedUpdate()
    {
        //ExtraGravityLogic();

        m_CurrVel_Y_Speed = Mathf.Abs(m_RigidBody2D.velocity.y);

        if (m_CurrVel_Y_Speed == 0f && m_PreviousVelocity_Y_Speed > 0f)
        {  

          // Can I imagine happen at apex of parabolic fall, and when landing into the ceiling. 
          // Consider caching velocity as a vector, and remember to think in terms of determinates and dot/cross products too. 
          // Caching speed, great for threshold compare, bad for landing state change.
          if(m_PreviousVelocity_Y_Speed > m_BigLandingSpeedThreshold )
          {
            OnBigLandEvent?.Invoke();
          }

         // Previous velocity should be negative but this is best controlled by a feet collision as platformers, may appear underneath.

        }

        m_PreviousVelocity_Y_Speed = m_CurrVel_Y_Speed;




    }

    public void HandleCoyoteTime()
    {
       // The moment we are no longer grounded, start the coyote timer.
       if(!m_IsGrounded)
       {
         m_CoyoteTimer -= Time.deltaTime;
       }
      else
       {
         m_CoyoteTimer = m_PlatformerMovement2DConfigSO.GetCoyoteTime();
      }
    }

    protected virtual void ExtraGravityLogic()
    {

      // Declare and initialize variables
      m_IsExtraGravityAllowed = !m_JetpackMechanics.IsJetPackRunning();

      // If time spent in air exceeds the extra gravity delay threshold then activate extra gravity.
      if(m_TimeSpentInAir > m_PlatformerMovement2DConfigSO.GetExtraGravityDelay() && m_IsExtraGravityAllowed)
      {
         m_RigidBody2D.gravityScale = m_PlatformerMovement2DConfigSO.GetExtraGravity();
      }
      else
      {
        m_RigidBody2D.gravityScale = m_OriginalGravityScale;
      }
      

    }

    public void Move(Vector2 movementVelocity) 
    {
       var unit = GetComponent<Unit>();
       if(unit != null)
       {
          var stunModifiers = unit.m_Modifiers.OfType<IStunMovementModifier>();
          if( stunModifiers != null)
          {
            if (stunModifiers.Count() > 0)
            {
              return;
            }
          }
       }
        m_RigidBody2D.velocity = new Vector2(movementVelocity.x, m_RigidBody2D.velocity.y);
    }

    public void Jump(Vector2 JumpForce) 
    {

      var unit = GetComponent<Unit>();
      if (unit != null)
      {
        var stunModifiers = unit.m_Modifiers.OfType<IStunMovementModifier>();
        if (stunModifiers != null)
        {
          if (stunModifiers.Count() > 0)
          {
            return;
          }
        }
      }
      m_LocomotionStateMachine.Jump();
      m_RigidBody2D.AddForce(JumpForce, ForceMode2D.Impulse);
      
    }

    private bool CheckGrounded()
    {

      Collider2D collider = Physics2D.OverlapBox(m_FeetTransform.position, m_FootSize, 0f, m_GroundLayer );
      bool isGrounded = collider != null;
      /*AbstractLocomotionState state = m_LocomotionStateMachine.GetCurrentLocomotionState();

      if( state != null )
      {
        if( state is InAirState)
        {
          isGrounded = false;
        }
      }*/

      return isGrounded;

    }

    public Vector2 GetFacing()
    {
      if (transform.localScale.x == -1)
      {
        return Vector2.left;
      }
      else
      {
        return Vector2.right;
      }
    }


  }
}
