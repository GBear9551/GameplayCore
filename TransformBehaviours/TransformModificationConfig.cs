using UnityEngine;

namespace GameDevTV
{
    [System.Serializable]
    public struct TransformModificationConfig 
    {
    [SerializeField] private float m_MinScale;
    [SerializeField] private float m_MaxScale;
    [SerializeField] private Vector3 m_minVectorScale;
    [SerializeField] private Vector3 m_maxVectorScale;

    [SerializeField] private float m_ScalingDirection;
    [SerializeField] private float m_ScalingSpeed;

    [SerializeField] private float m_RotationSpeed; // Degrees per second for ping pong rotation.
    [SerializeField] private float m_MinRotationSpeed; // Minimum rotation speed for randomization.
    [SerializeField] private float m_MaxRotationSpeed; // Maximum rotation speed for randomization.

    [SerializeField] private Vector3 m_minVectorRotation;
    [SerializeField] private Vector3 m_MaxVectorRotation;

    [SerializeField] private Vector3 m_minVectorPosition;
    [SerializeField] private Vector3 m_MaxVectorPosition;
    [SerializeField] private Vector3 m_VelocityDirection;

    // Scale
    public float GetMinScale() => m_MinScale;
    public void SetMinScale(float value) => m_MinScale = value;

    public float GetMaxScale() => m_MaxScale;
    public void SetMaxScale(float value) => m_MaxScale = value;

    public Vector3 GetMinVectorScale() => m_minVectorScale;
    public void SetMinVectorScale(Vector3 value) => m_minVectorScale = value;

    public Vector3 GetMaxVectorScale() => m_maxVectorScale;
    public void SetMaxVectorScale(Vector3 value) => m_maxVectorScale = value;


    // Scaling
    public float GetScalingDirection() => m_ScalingDirection;
    public void SetScalingDirection(float value) => m_ScalingDirection = value;

    public float GetScalingSpeed() => m_ScalingSpeed;
    public void SetScalingSpeed(float value) => m_ScalingSpeed = value;


    // Rotation Speed
    public float GetRotationSpeed() => m_RotationSpeed;
    public void SetRotationSpeed(float value) => m_RotationSpeed = value;

    public float GetMinRotationSpeed() => m_MinRotationSpeed;
    public void SetMinRotationSpeed(float value) => m_MinRotationSpeed = value;

    public float GetMaxRotationSpeed() => m_MaxRotationSpeed;
    public void SetMaxRotationSpeed(float value) => m_MaxRotationSpeed = value;


    // Rotation Vectors
    public Vector3 GetMinVectorRotation() => m_minVectorRotation;
    public void SetMinVectorRotation(Vector3 value) => m_minVectorRotation = value;

    public Vector3 GetMaxVectorRotation() => m_MaxVectorRotation;
    public void SetMaxVectorRotation(Vector3 value) => m_MaxVectorRotation = value;


    // Position Vectors
    public Vector3 GetMinVectorPosition() => m_minVectorPosition;
    public void SetMinVectorPosition(Vector3 value) => m_minVectorPosition = value;

    public Vector3 GetMaxVectorPosition() => m_MaxVectorPosition;
    public void SetMaxVectorPosition(Vector3 value) => m_MaxVectorPosition = value;

    public Vector3 GetVelocityDirection() => m_VelocityDirection;
    public void SetVelocityDirection(Vector3 value) => m_VelocityDirection = value;

  }
}
