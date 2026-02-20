using System;
using UnityEngine;
using UnityEngine.Events;
public class FollowPath : MonoBehaviour
{




  private LinePath m_path;
  private Rigidbody2D rb;
  private Vector3 m_currentWayPointPosition;
  private Vector3 m_nextWayPointPosition;
  private int m_mylastWayPointIndexVisited = -1;

  // Todo cycle through the path. m_onRepeatPath state.

  //public event 
  public event Action OnPathComplete;
  public event Action OnWayPointReached;

  private void Update()
  {
    // Declare and intiailize variables.

    // Check for edge cases and return if necessary.

    // Check to see if the object has reached the end of the path. If so, stop moving and return.
    // Invoke end of path logic.

    // Check proximity to the next waypoint on the path. 

    // Check for waypoint logic and update path if necessary.

    // Move to next waypoint on path.

  }


  public void SetPath(LinePath path)
  {
    m_path = path;
    
  }

  public void ReachedWayPoint()
  {

    // Declare and initialize variables.
      Vector3 directionToNextWayPoint = Vector3.zero;
      Vector3 directionNorm = Vector3.zero;

    // Check for edge cases and return if necessary.
      if(m_path == null)
      {
        Debug.LogError("Path is null. Please set the path before trying to move along it.");
        return;
      }

    // Update the index of the last waypoint visited to be the index of the current waypoint.
      m_mylastWayPointIndexVisited++;

    // Set the current waypoint.
      m_currentWayPointPosition = m_path.GetCurrentPointOnPathPosition(m_mylastWayPointIndexVisited);

    // Get the next waypoint on the path and set it as the target for movement.
      m_nextWayPointPosition = m_path.GetNextPointOnPath(m_mylastWayPointIndexVisited);

    // Get the direction from the current waypoint to the next waypoint.
      directionToNextWayPoint = m_nextWayPointPosition - m_currentWayPointPosition;
   
    // Normalize the direction vector to get the direction of movement.
      directionNorm = directionToNextWayPoint.normalized;

    // Get the rigidbody component of the object and set its velocity to be in the direction of movement and with a magnitude equal to the path speed.
       rb = GetComponent<Rigidbody2D>();

    // Set the velocity of the object to be in the direction of movement and with a magnitude equal to the path speed.
      rb.linearVelocity = directionNorm * m_path.GetPathSpeed();

    // SFX, animation, etc. logic for reaching a waypoint.
      OnWayPointReached?.Invoke();
  }

  public void ReachedEndOfPath()
  {
    OnPathComplete?.Invoke();
  }

  public void OnEnable()
  {
    m_mylastWayPointIndexVisited = -1;
  }
}
