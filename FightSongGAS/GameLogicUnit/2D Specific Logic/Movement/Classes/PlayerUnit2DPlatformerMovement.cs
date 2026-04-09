using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
  public class PlayerUnit2DPlatformerMovement : Unit2DPlatformerMovement
  {

    private PlayerInput2DPlatformerHandler m_PlayerInputHandler;
    private InputFrame m_InputFrame;

    protected override void Awake()
    {
      base.Awake();
      m_PlayerInputHandler = GetComponent<PlayerInput2DPlatformerHandler>();
    }

    protected override void Update()
    {
      // Gather Input
      m_InputFrame = m_PlayerInputHandler.m_InputFrame;

      base.Update();

      Jump();
      

      HandleSpriteFlip();
    }

    protected override void FixedUpdate()
    {
      base.FixedUpdate();
      Move();
    }


    protected void Jump()
    {

      bool isWithinCoyoteTime = m_CoyoteTimer > 0f;

      if(m_InputFrame.Jump && (m_IsGrounded || isWithinCoyoteTime) )
      {
        Vector2 JumpVector = Vector2.up * m_Unit.GetJumpForce();
        base.Jump(JumpVector);
      }
      else if( m_InputFrame.Jump && m_DoubleJumpAvailable)
      {
        Vector2 JumpVector = Vector2.up * m_Unit.GetDoubleJumpForce();
        base.Jump(JumpVector);
        m_DoubleJumpAvailable = false;
      }
    }

    protected void Move()
    {
      Vector2 moveVector = new Vector2(m_InputFrame.Movement.x * m_Unit.GetSpeed(), m_RigidBody2D.velocity.y);
      Move(moveVector);
    }

    private void HandleSpriteFlip()
    {
      Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

      if (mousePosition.x < transform.position.x)
      {
        //transform.eulerAngles = new Vector3(0f,-180f,0f);
        transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
      }
      else
      {

        //transform.eulerAngles = new Vector3(0f,0f,0f);
        transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
      }
    }
  }
}
