using System;
using UnityEngine;

namespace FightSongEventProgrammingSystem
{
  public static class EventBus<T> where T : IUnitEvent
  {

    // Event Binding ( bind the concept of event with an action to take when an event occurs )
    public static event Action<T> OnEvent;

    // Raise an Event
    public static bool Raise(T gameplayEvent)
    {
      if (gameplayEvent == null)
      {
        return false;
      }

      else
      {
        OnEvent?.Invoke(gameplayEvent);
        return true;
      }

    }

  } // End of class



} // End of namespace
