using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FightSongStateMachine
{
  public class LocomotionStateMachine : MonoBehaviour, ILocomotionStateContext
  {

    [SerializeField] public UnityEvent OnJumpEvent;
    [SerializeField] public UnityEvent OnCrouchEvent;
    [SerializeField] public UnityEvent OnGroundedEvent;
    [SerializeField] public UnityEvent OnInAirEvent;


    // Grounded State being entered on jump TODO
    private AbstractLocomotionState m_currentState;


    public AbstractLocomotionState GetCurrentLocomotionState()
    {
      return m_currentState;
    }

    private void Awake()
    {
      SetState(new GroundedState());
    }


    public virtual void SetState(AbstractLocomotionState newState)
    {
      if (newState == null)
      {
        Debug.LogWarning("Tried to set locomotion state to null.");
        return;
      }

      if( newState == m_currentState)
      {
        return;
      }

      m_currentState?.OnExit();

      m_currentState = newState;

      m_currentState.OnEnter();

      InvokeStateEvent(newState);

    }

      private void InvokeStateEvent(AbstractLocomotionState newState)
      {
        if (newState is CrouchingState)
        {
          OnCrouchEvent?.Invoke();
        }
        else if (newState is InAirState)
        {
          OnInAirEvent?.Invoke();
        }
        else if (newState is GroundedState)
        {
          OnGroundedEvent?.Invoke();
        }
      }

      // Walk

      // Run

      // 

      public void Jump()
      {
        m_currentState?.Jump(this);
        OnJumpEvent?.Invoke();
      }

      public void Crouch()
      {
        m_currentState?.Crouch(this);
      }

      public void Fall()
      {
        m_currentState?.Fall(this);
      }

      public void Land()
      {
        m_currentState?.Land(this);
      }
  }
}
