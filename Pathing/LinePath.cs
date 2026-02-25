using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LinePath : MonoBehaviour, IPath
{
  public GameObject WayPointPrefab { get; set; }
  public Waypoint StartingPoint { get; set; }
  public Waypoint EndingPoint { get; set; }
  public float PathSpeed { get; set; }
  public List<Waypoint> WayPointsOnPath { get; set; }
  public float PathLength { get; set; }
  [SerializeField] GameObject m_wayPointPrefab;
  [SerializeField] Waypoint m_startingPoint;
  [SerializeField] Waypoint m_endingPoint;
  [SerializeField] float m_pathSpeed = 1f;
  [SerializeField] List<Waypoint> m_wayPointsOnPath = new List<Waypoint>();
  private float m_pathLength;

  public void TraverseMethod()
  {
    // This method is not implemented yet, but it will contain the logic for traversing the path, such as moving an object along the path at a certain speed and handling any interactions with waypoints along the path.
  }

  private void Awake()
  {
    if(m_wayPointsOnPath.Contains(m_startingPoint) == false)
    {
      m_wayPointsOnPath.Add(m_startingPoint);
    }
    if(m_wayPointsOnPath.Contains(m_endingPoint) == false)
    {
      m_wayPointsOnPath.Add(m_endingPoint);
    }

  
  }

  private void Start()
  {
    CalculatePathLength();
    GenerateWayPointsOnPath(1);
  }

  public float GetPathSpeed()
  {
    return m_pathSpeed;
  }

  public Vector2 GetDirectionToEndPoint()
  {

    // Declare and initialize variables.
    Vector2 displacement = Vector2.zero;
    Vector2 dirNorm = Vector2.one;

    // Get the displacement vector from the starting point to the ending point.
    displacement = m_endingPoint.transform.position - m_startingPoint.transform.position;

    // Normalize the displacement vector to get the direction vector.
    dirNorm = displacement.normalized;

    return dirNorm;


  }

  public Vector3 GetStartingPoint()
  {
    return m_startingPoint.transform.position;
  }

  private List<Waypoint> GenerateWayPointsOnPath(int numberOfWaypoints)
  {

    // Declare and initialize variables.

    // Check for edge cases and return if necessary.
    if(numberOfWaypoints <= 2)
    {
      return m_wayPointsOnPath;
    }

    // Else the number of waypoint on the path has changed and needs updating. 
    else
    {

 
 
      return m_wayPointsOnPath;
    }
  }



  private void OnDrawGizmos()
  {

    // Declare and initialize variables.
    int currentPointIndex = 0;
    int numberOfPoints = m_wayPointsOnPath.Count;
    Vector3 currentPoint = Vector3.zero;
    Vector3 nextPoint = Vector3.zero;

    // Set the gizmo color to red so that the path is visible in the scene view.
    Gizmos.color = Color.indianRed;

    // Get the current point on the path for the first iteration of the loop.
    currentPoint = GetCurrentPointOnPathPosition(currentPointIndex);

    // Loop through every point, but the last point.
    for (currentPointIndex = 0; currentPointIndex < numberOfPoints-1; currentPointIndex++)
    {

       // Get the next point on the path.
         nextPoint = GetNextPointOnPath(currentPointIndex);
     
       // Draw a line between the current point and the next point to visualize the path.
         Gizmos.DrawLine(currentPoint, nextPoint);

       // Update the current point on the path for the next iteration of the loop.
          currentPoint = nextPoint;

    }



  }

  public Vector3 GetCurrentPointOnPathPosition(int currentPointIndex)
  {
    // Declare and initialize variables.
    Vector3 currentPointPositionOnPath = Vector3.zero;
    // Check for edge cases and return if necessary.
    if (currentPointIndex >= m_wayPointsOnPath.Count || currentPointIndex < 0)
    {
      return Vector3.zero;
    }
    currentPointPositionOnPath = m_wayPointsOnPath[currentPointIndex].transform.position;
    return currentPointPositionOnPath;
  }

  public Vector3 GetNextPointOnPath(int currentPointIndex)
  {
    // Declare and initialize variables.
    Vector3 nextPointPositionOnPath = Vector3.zero;

    // Check for edge cases and return if necessary.
    if( currentPointIndex >= m_wayPointsOnPath.Count - 1 || currentPointIndex < 0)
    {
      return Vector3.zero;
    }

    nextPointPositionOnPath = m_wayPointsOnPath[currentPointIndex + 1].transform.position;

    return nextPointPositionOnPath;
  }

  

  private void CalculatePathLength()
  {    
   
    // Declare and initialize variables.
    int currentPointIndex = 0;
    int numberOfPoints = m_wayPointsOnPath.Count;
    Vector3 currentPoint = Vector3.zero;
    Vector3 nextPoint = Vector3.zero;


    // Get the current point on the path for the first iteration of the loop.
    currentPoint = GetCurrentPointOnPathPosition(currentPointIndex);

    // Loop through every point, but the last point.
    for (currentPointIndex = 0; currentPointIndex < numberOfPoints - 1; currentPointIndex++)
    {

      // Get the next point on the path.
      nextPoint = GetNextPointOnPath(currentPointIndex);

      // Calculate the distance between the current point and the next point and add it to the total path length.
      m_pathLength += Vector3.Distance(currentPoint, nextPoint);

      // Update the current point on the path for the next iteration of the loop.
      currentPoint = nextPoint;
    }
  

  }


}
