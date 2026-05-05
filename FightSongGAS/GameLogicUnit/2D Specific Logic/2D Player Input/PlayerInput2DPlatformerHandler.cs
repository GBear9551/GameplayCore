using FightSongGameLogicSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput2DPlatformerHandler : MonoBehaviour
{

  private PlayerPlatformerMovementActions m_PlayerActions;
  private InputAction m_Move;
  private InputAction m_Jump;

  public InputFrame InputFrame { get; private set; }



   void Awake()
  {
     
     m_PlayerActions = new PlayerPlatformerMovementActions();
     m_Move = m_PlayerActions.Movement.Move;
     m_Jump = m_PlayerActions.Movement.Jump;
 

  }

  private void OnEnable()
  {
    m_PlayerActions.Enable();
  }


  private void OnDisable()
  {
    m_PlayerActions.Disable();
  }

  private void Update()
  {
      // Gather input.
      InputFrame = GatherInput();

      Debug.Log("Input frame: " +  InputFrame.Movement.x + "  " + InputFrame.Movement.y);

  }

  private InputFrame GatherInput()
  {
    return new InputFrame
    {
      Movement = m_Move.ReadValue<Vector2>(),
      Jump = m_Jump.WasPressedThisFrame()
    };
  }

}



public struct InputFrame
{
   public Vector2 Movement;
   public bool Jump;
}
