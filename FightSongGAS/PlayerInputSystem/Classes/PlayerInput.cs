using UnityEngine;
using UnityEngine.Events;
namespace FightSongGameLogicSystem
{
    public abstract class PlayerInput : MonoBehaviour, IPlayerInput
    {

      //

      //Exposed func plugs.
      public UnityEvent<GameObject> OnKeyPressed;

    }
}
