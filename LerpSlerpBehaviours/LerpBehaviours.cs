using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

namespace GameDevTV
{
    public class LerpBehaviours : MonoBehaviour
    {
      [Header("Lerp & Slerp Configurations")]
      [SerializeField] LerpSlerpConfig m_LerpSlerpConfig;
      /*[SerializeField] private Transform m_LeftTransform;
      [SerializeField] private Transform m_RightTransform;
      [SerializeField] private float m_LerpSpeed = 1f;
      [SerializeField] private float m_SlerpRotationSpeed = 1f;*/


      private Coroutine m_LerpCoroutine;
      private bool m_cancelCoroutine = false;
      public UnityEvent<GameObject> OnStartBehaviour;

    private void OnEnable()
    {
      OnStartBehaviour?.Invoke(this.gameObject);
    }

    public void Init(LerpSlerpConfig otherConfig)
    {
      m_LerpSlerpConfig = otherConfig;
    }


    public void StartLerpWithOwnership(GameObject gameObj)
      {
          var ownershipComponent = gameObj.GetComponent<LerpBehaviours>();
          
          if (ownershipComponent != null) 
          {
              ownershipComponent.StartLerp(gameObj);
          } 
          else 
          {
              var newLerpComp = gameObj.AddComponent<LerpBehaviours>();
              var lerpSpeed = Random.Range(1f, 5f);
              newLerpComp.Init(m_LerpSlerpConfig);
              newLerpComp.StartLerp(gameObj);
          }
      }

    public void StartSLerpWithOwnership(GameObject gameObj)
    {
      var ownershipComponent = gameObj.GetComponent<LerpBehaviours>();

      if (ownershipComponent != null)
      {
        ownershipComponent.StartSlerp(gameObj);
      }
      else
      {
        var newLerpComp = gameObj.AddComponent<LerpBehaviours>();
        var SLerpSpeed = Random.Range(1f, 5f);
        newLerpComp.Init(m_LerpSlerpConfig);
        newLerpComp.StartSlerp(gameObj);
      }
    }

    public void StartLerp(GameObject gameObj)
      {
          if (m_LerpCoroutine != null)
          {
              StopCoroutine(m_LerpCoroutine);
          }
          m_LerpCoroutine = StartCoroutine(LerpRoutine(gameObj));
      }

    public void StartSlerp(GameObject gameObj)
    {
          if(m_LerpCoroutine != null)
          {
              StopCoroutine(m_LerpCoroutine);
          }
          m_LerpCoroutine = StartCoroutine(SlerpRoutine(gameObj));
    }

    private IEnumerator SlerpRoutine(GameObject gameObj)
    {
       // Declare and initialize variables.
         float time = 0f;
         Quaternion startRotation = gameObj.transform.rotation;
         Quaternion halfTurn = Quaternion.AngleAxis(180f, Vector3.up);
         Quaternion middleRotation = startRotation * halfTurn;
         Quaternion endRotation = middleRotation * halfTurn;
         float slerpRotationSpeed = m_LerpSlerpConfig.GetSlerpRotationSpeed();

       // While the coroutine has not received a cancel coroutine token continue to loop.
        while(m_cancelCoroutine == false)
        {
          time = 0f;

        
        
          // First, slerp rotation.
          while(time < 1f)
          {
            gameObj.transform.rotation = Quaternion.Slerp(startRotation, middleRotation, time);
            time += Time.deltaTime * slerpRotationSpeed;

            yield return null;
          }

          // Second, slerp rotation.
          time = 0f;

          while(time < 1f)
          {
            gameObj.transform.rotation = Quaternion.Slerp(middleRotation, endRotation, time);
            time += Time.deltaTime * slerpRotationSpeed;
            yield return null;
          }

        // function stubb
          yield return null;
        }
          
    }

      private IEnumerator LerpRoutine(GameObject gameObj)
      {
        // Declare and initialize variables.
          float time = 0f;
          float lerpSpeed = m_LerpSlerpConfig.GetLerpSpeed();
          Vector3 startPosition = Vector3.zero;
          Vector3 leftPosition = m_LerpSlerpConfig.GetLeftTransform().position;
          Vector3 rightPosition = m_LerpSlerpConfig.GetRightTransform().position;
 

        // While the coroutine has not been stopped, continue to loop, repeat
        while(m_cancelCoroutine == false)
        { 

          // Set the start position to the current position of the object at the beginning of each loop iteration.
            startPosition = gameObj.transform.position;
            time = 0f;

          // Loop until the object is close enough to the target position.
          while(time < 1f)
          {

              gameObj.transform.position = Vector3.Lerp(startPosition, leftPosition, time);
              time += Time.deltaTime * lerpSpeed;
              yield return null;
              
          }
     
          // Change the start position to the current position and reset time for the next lerp.
          startPosition = gameObj.transform.position;
          time = 0f;
          while( time < 1f)
          {

              gameObj.transform.position = Vector3.Lerp(startPosition, rightPosition, time);
              time += Time.deltaTime * lerpSpeed;
              yield return null;
          }
        }

        yield return null;
      }


      

  }
}
