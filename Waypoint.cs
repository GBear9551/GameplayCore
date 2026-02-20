using UnityEngine;
using UnityEngine.Events;
public class Waypoint : MonoBehaviour
{

    public UnityEvent onWayPointReached;
    public bool isEndpoint = false;

  public void OnTriggerEnter2D(Collider2D collision)
  { 
        var otherIsFollowingPath = collision.GetComponent<FollowPath>();
        var otherIsABall = collision.GetComponent<BallGameRules>();

        if (otherIsFollowingPath != null && otherIsABall != null)
        {
            onWayPointReached?.Invoke();
            
            if(isEndpoint && otherIsFollowingPath.enabled)
            {
                otherIsFollowingPath.ReachedEndOfPath();
            }
            else if(otherIsFollowingPath.enabled)
            {
                otherIsFollowingPath.ReachedWayPoint();
            }
    }
  }

}
