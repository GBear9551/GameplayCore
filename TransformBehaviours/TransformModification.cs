using System.Collections;
using UnityEngine;

public class TransformModification : MonoBehaviour
{

   [Header("Transform Modification Setting")]
   [Tooltip("Can call coroutines on a set of objects, stop this component to stop those coroutines.")]
   [SerializeField] float m_MinScale = 0.2f;
   [SerializeField] float m_MaxScale = 1.5f;
   [SerializeField] Vector3 m_minVectorScale = Vector3.one;
   [SerializeField] Vector3 m_maxVectorScale = Vector3.one;
   [SerializeField] float m_ScalingDirection = -1;
   [SerializeField] float m_ScalingSpeed = 1f;
   [SerializeField] public float RotationSpeed = 30f; // Degrees per second for ping pong rotation.
   [SerializeField] float m_MinRotationSpeed = 45f; // Minimum rotation speed for randomization.
    [SerializeField] float m_MaxRotationSpeed = 200f; // Maximum rotation speed for randomization.

  [SerializeField] Vector3 m_minVectorRotation = Vector3.zero;
   [SerializeField] Vector3 m_MaxVectorRotation = Vector3.zero;

   [SerializeField] Vector3 m_minVectorPosition = Vector3.zero;
   [SerializeField] Vector3 m_MaxVectorPosition = Vector3.zero;
   





   public void ApplyRandomScalingToThis()
   {
    var randomVal = Random.Range(m_MinScale, m_MaxScale);
    transform.localScale = new Vector3(randomVal, randomVal, randomVal);
   }

   public void ApplyPingPongScalingToOther(GameObject otherGO)
   {
      if (otherGO != null)
      {

        var otherGameObjectTransformModificationComp = otherGO.GetComponent<TransformModification>();

        if (otherGameObjectTransformModificationComp != null)
        {
          otherGameObjectTransformModificationComp.ApplyScalingPingPong();
        }
        else
        {
          Debug.LogError("Other GameObject does not have a TransformModification component! Cannot apply ping pong scaling.");
        }
 
      }
      else
      {
      Debug.LogError("Other GameObject is null! Cannot apply ping pong scaling.");
      }

  }

  public void RandomizeRotationSpeed(GameObject other)
  {
    if (other != null)
    {
      var otherTransformModificationComp = other.GetComponent<TransformModification>();
      if (otherTransformModificationComp != null)
      {
        otherTransformModificationComp.RotationSpeed = Random.Range(m_MinRotationSpeed, m_MaxRotationSpeed); // Random rotation speed between 10 and 100 degrees per second.
      }
      else
      {
        Debug.LogError("Other GameObject does not have a TransformModification component! Cannot randomize rotation speed.");
      }
    }
    else
    {
      Debug.LogError("Other GameObject is null! Cannot randomize rotation speed.");
    }
  }

  public void ApplyPingPongRotationToOther(GameObject otherGO)
  {
    if (otherGO != null)
    {

      var otherGameObjectTransformModificationComp = otherGO.GetComponent<TransformModification>();

      if (otherGameObjectTransformModificationComp != null)
      {
        otherGameObjectTransformModificationComp.ApplyRotationPingPong();
      }
      else
      {
        Debug.LogError("Other GameObject does not have a TransformModification component! Cannot apply ping pong scaling.");
      }

    }
    else
    {
      Debug.LogError("Other GameObject is null! Cannot apply ping pong scaling.");
    }

  }

  public void ApplyRotationPingPong()
  {
    StartCoroutine(ApplyRotationPingPongRoutine());
  }

  private IEnumerator ApplyRotationPingPongRoutine()
  {
    while(true)
    {
      transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
      yield return null;
    }
  }

  private IEnumerator ApplyPingPongScalingToOtherRoutine(GameObject otherGO)
   {
    // Declare and initialize local variables, specific to the ApplyPingPongScalingToOtherRoutine method scope.
    float scalingDirection = m_ScalingDirection;

    // Constraints: Ensure the game object's scale remains within the specified min and max limits.
    
    // Scale the game object up and down between min and max scale using a function over time.
    while (true)
    {

      // Constraints: Ensure the ball's scale remains within the specified min and max limits.
      if (otherGO.transform.localScale.x >= m_MaxScale || otherGO.transform.localScale.x <= m_MinScale)
      {
        // Reverse scaling direction if limits are reached.
        scalingDirection = -scalingDirection;
      }


      // Scale the ball up and down between min and max scale using a function over time.
      otherGO.transform.localScale += Vector3.one * scalingDirection * m_ScalingSpeed * Time.deltaTime;

      yield return null;
    }

   }

  public void ApplyRandomRotationToThis()
   {
    var xRotation = Random.Range(m_minVectorRotation.x, m_MaxVectorRotation.x);
    var yRotation = Random.Range(m_minVectorRotation.y, m_MaxVectorRotation.y);
    var zRotation = Random.Range(m_minVectorRotation.z, m_MaxVectorRotation.z);
    transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
  }

   public void ApplyRandomScalingToOther(GameObject gameObject)
   {
     var xScale = Random.Range(m_minVectorScale.x, m_maxVectorScale.x);
     var yScale = Random.Range(m_minVectorScale.y, m_maxVectorScale.y);
     var zScale = Random.Range(m_minVectorScale.z, m_maxVectorScale.z);

     gameObject.transform.localScale = new Vector3(xScale, yScale, zScale);
   }

   public void ApplyRandomUniformScalingToOther(GameObject gameObject)
   {
      var randomVal = Random.Range(m_MinScale, m_MaxScale);
      gameObject.transform.localScale = new Vector3(randomVal, randomVal, randomVal); 
   }

   public void ApplyRandomRotationToOther(GameObject gameObject)
   {
      var xRotation = Random.Range(m_minVectorRotation.x, m_MaxVectorRotation.x);
      var yRotation = Random.Range(m_minVectorRotation.y, m_MaxVectorRotation.y);
      var zRotation = Random.Range(m_minVectorRotation.z, m_MaxVectorRotation.z);
      gameObject.transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
   }

   public void ApplyRandomPositionToOther(GameObject gameObject)
   {
    
      var xPosition = Random.Range(m_minVectorPosition.x,m_MaxVectorPosition.x);
      var yPosition = Random.Range(m_minVectorPosition.y,m_MaxVectorPosition.y);
      var zPosition = Random.Range(m_minVectorPosition.z, m_MaxVectorPosition.z);
      gameObject.transform.localPosition = new Vector3(xPosition, yPosition, zPosition);

 
   }

   public void ApplyScalingPingPong()
   {
      StartCoroutine(ApplyScalingPingPongRoutine());
   }

   public IEnumerator ApplyScalingPingPongRoutine()
   {


      while(true)
      {
         
        // Constraints: Ensure the ball's scale remains within the specified min and max limits.
        if (transform.localScale.x >= m_MaxScale || transform.localScale.x <= m_MinScale)
        {
          // Reverse scaling direction if limits are reached.
          m_ScalingDirection = -m_ScalingDirection;
        }


        // Scale the ball up and down between min and max scale using a function over time.
        transform.localScale += Vector3.one * m_ScalingDirection * m_ScalingSpeed * Time.deltaTime;
 
        yield return null;
      }

   }
  /*
    Name: ScaleBall
    Input: None
    Ouput: None
    Dependencies: MonoBehaviour
    Process: Scales the ball up and down between min and max scale using a function over time.
  */
  private void ScaleBall()
  {
    // Declare and initialize local variables, specific to the ScaleBall method scope.
    Vector3 newScale = Vector3.zero;



  }
}
