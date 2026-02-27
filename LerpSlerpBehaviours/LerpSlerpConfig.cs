using UnityEngine;

namespace GameDevTV
{
    [System.Serializable]
    public struct LerpSlerpConfig  
    {

    [SerializeField] private Transform m_LeftTransform;
    [SerializeField] private Transform m_RightTransform;
    [SerializeField] private float m_LerpSpeed;
    [SerializeField] private float m_SlerpRotationSpeed;
    [SerializeField] private AnimationCurve m_AnimationCurve; 

    public AnimationCurve GetAnimationCurve()
    {
      return m_AnimationCurve;
    }

    public void SetAnimationCurve(AnimationCurve animationCurve)
    {
      m_AnimationCurve = animationCurve;
    }

    // Left Transform
    public Transform GetLeftTransform()
    {
      return m_LeftTransform;
    }

    public void SetLeftTransform(Transform value)
    {
      m_LeftTransform = value;
    }


    // Right Transform
    public Transform GetRightTransform()
    {
      return m_RightTransform;
    }

    public void SetRightTransform(Transform value)
    {
      m_RightTransform = value;
    }


    // Lerp Speed
    public float GetLerpSpeed()
    {
      return m_LerpSpeed;
    }

    public void SetLerpSpeed(float value)
    {
      m_LerpSpeed = value;
    }


    // Slerp Rotation Speed
    public float GetSlerpRotationSpeed()
    {
      return m_SlerpRotationSpeed;
    }

    public void SetSlerpRotationSpeed(float value)
    {
      m_SlerpRotationSpeed = value;
    }
  }
}
