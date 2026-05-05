using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace FightSongAnimationSystem
{
  public class Player2DUnitAnimationPresenter : Unit2DAnimationPresenter
  {


    [SerializeField] float m_PlayerLeanAngle = 45f;
    [SerializeField] Transform m_HatTransform;

    private Coroutine m_LeanRoutine;
    private Coroutine m_HatLeanRoutine;
    private Quaternion m_PreviousLean = Quaternion.identity;
    private Quaternion m_PreviousHatLean = Quaternion.identity;
    

    

    protected override void Awake()
    {
      base.Awake();

      m_LocomotionStateMachine.OnInAirEvent.AddListener(PlayInAirLeanEffect);
      
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      m_LocomotionStateMachine.OnInAirEvent.RemoveListener(PlayInAirLeanEffect);
    }

    protected override void PlayLandEffect()
    {
        base.PlayLandEffect();
    }


    protected override void PlayInAirEffect()
    {
      StopWalkEffect();
      base.PlayInAirEffect();
      PlayInAirLeanEffect();
    }

    protected virtual void PlayInAirLeanEffect()
    {
      ApplyAndSetLean(GetLeanDirection());
    }

    protected override void PlayWalkEffect()
    {
        ApplyAndSetLean(GetLeanDirection());
        base.PlayWalkEffect();
    }

    protected override void PlayJumpEffect()
    {
      ApplyAndSetLean(GetLeanDirection());
      base.PlayJumpEffect();
    }

    private void ApplyAndSetLean(Quaternion targetLean)
    {
      // Get current target lean
      Quaternion currLean = targetLean;
      Quaternion currHatLean = targetLean * Quaternion.Inverse(m_HatTransform.rotation);
      float hatAngle = Quaternion.Angle(currHatLean, Quaternion.identity);
      hatAngle = Mathf.Clamp(hatAngle, -18.5f, 18.5f);
      Quaternion hatTargetOrientation = Quaternion.AngleAxis(hatAngle, Vector3.forward);

      // Apply lean based on facing and motion direction vectors.
      if (currLean != m_PreviousLean)
      {
        ApplyPlayerLean(currLean);
        m_PreviousLean = currLean;
      }
      if (currHatLean != m_PreviousHatLean)
      {
        ApplyHatLean(hatTargetOrientation);
        m_PreviousHatLean = currHatLean;
      }
    }

    protected override void StopWalkEffect()
    {
      base.StopWalkEffect();
      ApplyAndSetLean(Quaternion.identity);
    }

    private void ApplyPlayerLean(Quaternion targetRotation)
    {

      if (m_LeanRoutine != null)
      {
        StopCoroutine(m_LeanRoutine);
      }

      m_LeanRoutine = StartCoroutine(ApplyPlayerLeanRoutine(targetRotation));

    }

    private void ApplyHatLean(Quaternion targetRotation)
    {
         if (m_HatLeanRoutine != null)
         {
           StopCoroutine(m_HatLeanRoutine);
         }

         m_HatLeanRoutine = StartCoroutine(ApplyHatLeanRoutine(targetRotation));  
    }

    private IEnumerator ApplyHatLeanRoutine(Quaternion targetRotation)
    {
      float slerpSpeed = 1f;
      float time = 0f;

      

      while(time < 1f)
      {

        m_HatTransform.rotation = Quaternion.Slerp(m_HatTransform.rotation, targetRotation, time);
        time += Time.deltaTime * slerpSpeed;

        yield return null;
      }

      yield return null;

    }

    private IEnumerator ApplyPlayerLeanRoutine(Quaternion targetRotation)
    {

      float slerpSpeed = 1f;
      float time = 0f;


      // If we are facing to the right
      while(time < 1f)
      { 
        
        m_MainVisualUsedToModify.rotation = Quaternion.Slerp(m_MainVisualUsedToModify.rotation, targetRotation, time);
        time += Time.deltaTime * slerpSpeed;
        yield return null;
      }


      yield return null;

     }

    private Quaternion GetLeanDirection()
    {
        
       // Declare and intialize variables

         // Target lean orientation 
         Quaternion targetLean = Quaternion.identity;

         // Which way is the player 2d unit facing?
         Vector2 facingDirection = PlayerController.Instance.GetFacing().normalized;

         // Which was it the player 2d unit moving?
         Vector2 motionDirection = new Vector2(m_RigidBody2D.velocity.x,0).normalized;

         // In regards to the directionality of the two vectors: facing and motion, are they perpendicular? opposite? or similiar?
         float directionality = Vector2.Dot(motionDirection, facingDirection);

      // If the two vectors are similar then we want to lean in that direction
      if (directionality > 0.99f)
      {
         if(facingDirection == Vector2.right)
         {
            targetLean = Quaternion.AngleAxis(-m_PlayerLeanAngle, Vector3.forward);
         }
         else
         {
            targetLean = Quaternion.AngleAxis(m_PlayerLeanAngle, Vector3.forward);
         }
      }

      // Else if the two vectors are opposite then we want to lean in the opposite direction we are facing.
      else if (directionality < -0.98f)
      {
        if (facingDirection == Vector2.right)
        {
          targetLean = Quaternion.AngleAxis(m_PlayerLeanAngle, Vector3.forward);
        }
        else
        {
          targetLean = Quaternion.AngleAxis(-m_PlayerLeanAngle, Vector3.forward);
        }
      }
      else if (directionality < 0.01f && directionality > -0.01f)
      {
        //targetLean = m_PreviousLean;
      }



        return targetLean;
    }

    // Not dot product
/*    private Quaternion GetLeanDirection()
    {

      // Dot product to detect direction alignment.

      Vector2 facingDirection = PlayerController.Instance.GetFacing();

      Vector2 motionDirection = m_RigidBody2D.velocity.normalized;

      Vector2 result = facingDirection - motionDirection;

      Quaternion targetRotation = Quaternion.identity;


        if (facingDirection == Vector2.right)
        {

          if (result == Vector2.zero)
          {
            // facing the same way as motion
            targetRotation = Quaternion.FromToRotation(Vector2.right, Vector2.right - new Vector2(1f / 2f, 1f / 2f));


          }
          else
          {

            // facing the same way as motion
            targetRotation = Quaternion.FromToRotation(Vector2.right, new Vector2(1f / 2f, 1f / 2f));

          }

        }

        else if (facingDirection == Vector2.left)
        {

          if (result == Vector2.zero)
          {
            // facing the same way as motion

            targetRotation = Quaternion.FromToRotation(Vector2.left, - new Vector2(1f / 2f, 1f / 2f));
          }
          else
          {

            // facing the same way as motion
            targetRotation = Quaternion.FromToRotation(Vector2.left, Vector2.left +  new Vector2(1f / 2f, 1f / 2f));

          }
        }

        return targetRotation;
      }*/

  }
}
