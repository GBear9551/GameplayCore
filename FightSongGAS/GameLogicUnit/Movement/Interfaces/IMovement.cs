using UnityEngine;

namespace FightSongGameLogicSystem 
{
    // Declare movement behaviour/functions
    public interface IMovement
    {
       void Move(Vector3 direction, float speed);
    }
}
