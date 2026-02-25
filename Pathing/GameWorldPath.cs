using System.Collections.Generic;
using UnityEngine;

public class GameWorldPath : MonoBehaviour, IGameWorldPath 
{

    public GameObject WayPointPrefab { get; set; }
    [field:SerializeField] public Waypoint StartingPoint { get; set; }
    [field:SerializeField] public Waypoint EndingPoint { get; set; }
    [field:SerializeField] public float PathSpeed { get; set; }
    [field:SerializeField] public List<Waypoint> WayPointsOnPath { get; set; }
    public float PathLength { get; set; }

    public virtual Vector3 GetCurrentPointOnPathPosition(int index) {  return Vector3.zero; }
    public virtual Vector3 GetNextPointOnPath(int index) { return Vector3.zero; }
    public virtual float GetPathSpeed() { return PathSpeed; }
    public virtual void TraverseMethod()
    {
       Debug.Log("Base Class Traverse Method Called");
    }
}
