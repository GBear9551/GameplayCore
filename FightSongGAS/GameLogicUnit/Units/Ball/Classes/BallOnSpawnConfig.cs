using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct BallOnSpawnConfig 
{

   [SerializeField] private Vector3 m_DirectionToSendOnSpawn;
   [SerializeField] private float m_SpeedToSendOnSpawn;
   [SerializeField] private List<ColorSO> m_possibleBallColors; // List of possible colors for the ball, set in the Unity Editor. The color will be randomly selected from this list when the ball is spawned.
   [SerializeField] private IGameWorldPath m_Path;
   [SerializeField] private bool m_RandomizeDirection;

   public int GetPossibleBallColorsCount() => m_possibleBallColors.Count; // Get the count of possible ball colors in the list, which can be used to determine how many different colors the ball can have when it is spawned
   public ColorSO GetPossibleBallColorAtIndex(int index) => m_possibleBallColors[index]; // Get a specific color from the list of possible ball colors based on the provided index, which can be used to assign a specific color to the ball when it is spawned
   public Vector3 GetDirectionToSendOnSpawn() => m_DirectionToSendOnSpawn; // Get the direction in which to send the ball when it is spawned, which can be used to apply an initial force to the ball in that direction
   public float GetSpeedToSendOnSpawn() => m_SpeedToSendOnSpawn; // Get the speed at which to send the ball when it is spawned, which can be used to determine how fast the ball should move when it is spawne
   public Vector3 GetLinear2DRigidBodyVelocity() => m_DirectionToSendOnSpawn.normalized * m_SpeedToSendOnSpawn; // Get the velocity to apply to the ball's Rigidbody2D component when it is spawned, which is calculated by normalizing the direction and multiplying it by the speed to send on spawn. This velocity can be used to set the initial movement of the ball when it is spawned from the pool.                                                              

}
