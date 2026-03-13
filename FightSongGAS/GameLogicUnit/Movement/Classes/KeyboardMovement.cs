using UnityEngine;

namespace FightSongGameLogicSystem
{
    public class KeyboardMovement : Movement 
    {
       public override void Move(Vector3 direction, float speed)
       {

          //base.Move(direction, speed);

          this.gameObject.transform.position += new Vector3(direction.x, direction.y, direction.z) * speed * Time.deltaTime;


       }
   
    }
}
