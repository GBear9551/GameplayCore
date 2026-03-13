using UnityEngine;

namespace FightSongGameLogicSystem
{
    public abstract class Movement : MonoBehaviour, IMovement
    {
      public virtual void Move(Vector3 direction, float speed)
      {
        //throw new System.NotImplementedException();
      }

    }
}
