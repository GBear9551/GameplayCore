using System.Collections.Generic;
using UnityEngine;

public interface IGameWorldPath
{
  public GameObject WayPointPrefab { get; set; }
  public Waypoint StartingPoint { get; set; }
  public Waypoint EndingPoint { get; set; }
  public float PathSpeed { get; set; }
  public List<Waypoint> WayPointsOnPath { get; set; }
  public float PathLength { get; set; }

  public void TraverseMethod();
  public Vector3 GetCurrentPointOnPathPosition(int index);
  public Vector3 GetNextPointOnPath(int index);
  public float GetPathSpeed();
}
