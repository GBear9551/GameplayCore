// Unity Header Files ( code and data located in other places in the filesystem)
using System.Collections;
using UnityEngine;

// Namespaces: Identifiers used to access other pieces of code and data.
namespace GameDevTV.Ball
{


  // Key Entry Point: Class Definition and Implementation 
  public class BallMovement : MonoBehaviour
  {

    // Important programmer ( data ) obtained from MonoBehaviour
      // Ball Position
      // Ball Rotation
      // Ball Scale
      // Delta Time

    // programmer ( data ) specific to BallMovement class
      // Original Starting Position
      private Vector3 m_originalStartingPosition;

      // Original Starting Rotation
      private Quaternion m_originalStartingRotation;

      // Original Starting Scale
      private Vector3 m_originalStartingScale;


    // Important Game Designer ( data ) ( SerializedFields Game Editor Inspector ) 
    [Header("Ball Movement Settings")]
   
    //  Movement Speed
    [SerializeField] public float m_movementSpeed = 5f;

    //  Movement Direction
    [SerializeField] public Vector3 m_movementDirection = Vector3.zero;

    //  Distance to travel until direction change
    [SerializeField] public float m_distanceUntilDirectionChange = 5f;

    //  Axis of Rotation
    [SerializeField] public Vector3 m_rotationAxis = Vector3.up;

    //  Rotation Speed of ball
    [SerializeField] public float m_rotationSpeed = 180f;

    //  Rotation to complete until direction change
    [SerializeField] public float m_rotationUntilDirectionChange = 360f;

    //  Minimum Ball Scale
    [SerializeField] public float m_minBallScale = 0.5f;

    //  Max Ball Scale
    [SerializeField] public float m_maxBallScale = 2f;

    // Scaling Direction
    [SerializeField] public int m_scalingDirection = 1;

    //  Ball Scaling Speed
    [SerializeField] public float m_ballScalingSpeed = 1f;

    // Key Unity Methods ( code ) 


    /*
      Name: Start
      Input: None
      Ouput: None
      Dependencies: MonoBehaviour
      Process: Called before the first frame update to get the ball's original starting position, rotation, and scale.
    */
    private void Start()
    {
      // Declare and initialize local variables, specific to the Start method scope, overriding Start method from MonoBehaviour.



    }


    /*
     Name: Update
     Input: None 
     Ouput: None
     Dependencies: MonoBehaviour
     Process: Called once per frame to update the ball's movement, rotation, and scaling, the number of calls depends
              on the user's processor speed - CPU. 
    */

    private void Update()
    {

      // Declare and initialize local variables, specific to the Update method scope, overriding Update method from MonoBehaviour.

      // Move the ball.
        // function: MoveBall()
          //MoveBall();

      // Rotate the ball.
        // function: RotateBall()
          //RotateBall();

      // Scale the ball.
        // function: ScaleBall()
          //ScaleBall();


    }



    public void OnDisable()
    {
      //StopCoroutine(MoveBallRoutine());
    }
    /*
     Name: MoveBallRoutine
     Input: Vector3  MovementDirection,  float MovementSpeed, float DistanceUntilDirectionChange
     Ouput: None
     Dependencies: MonoBehaviour, Systems, MoveBall()
     Process: Coroutine that continuously moves the ball by calling MoveBall() every frame, yielding control back to the main loop.
    */
    public IEnumerator MoveBallRoutine(Vector3 movementDirection, float movementSpeed, float distanceUntilDirectionChange)
    {
      while (true) // Infinite loop to keep the coroutine running indefinitely.
      {
        MoveBall(movementDirection, movementSpeed, distanceUntilDirectionChange); // Call the MoveBall method to update the ball's position.
        yield return null; // Yield control back to the main loop until the next frame, allowing other processes to run.
      }
    }

    /*
    Name: MoveBall
    Input: Vector3 movementDirection, float movementSpeed, float distanceUntilDirectionChange
    Ouput: None
    Dependencies: MonoBehaviour
    Process: Translates the ball's position based on movement speed, direction, and delta time.
    */
    private void MoveBall(Vector3 movementDirection, float movementSpeed, float distanceUntilDirectionChange)
    {
      // Declare and initialize local variables, specific to the MoveBall method scope.
      Vector3 displacementFromStart = Vector3.zero;
      float distanceTraveled = 0;

      // Calculate the distance traveled since the original position.
      displacementFromStart = transform.position - m_originalStartingPosition;
      distanceTraveled = displacementFromStart.magnitude;

      // Constraint: Check if the ball has traveled the specified distance to change direction.
      if (distanceTraveled >= distanceUntilDirectionChange)
      {
        // If so, reverse the movement direction.
        movementDirection = -movementDirection;
      }

      // Translate the ball position based on movement speed, direction, and delta time.
      transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);
    }


    /*
     Name: MoveBallRoutine
     Input: None
     Ouput: None
     Dependencies: MonoBehaviour, Systems, MoveBall()
     Process: Coroutine that continuously moves the ball by calling MoveBall() every frame, yielding control back to the main loop.
    */
    public IEnumerator MoveBallRoutine()
    {

      // Store initial-original position, rotation, and scale of the ball if needed for future reference.
      m_originalStartingPosition = transform.position;


      while (true) // Infinite loop to keep the coroutine running indefinitely.
      {
        MoveBall(); // Call the MoveBall method to update the ball's position.
        yield return null; // Yield control back to the main loop until the next frame, allowing other processes to run.
      }
    }


    /*
    Name: MoveBall
    Input: None 
    Ouput: None
    Dependencies: MonoBehaviour
    Process: Translates the ball's position based on movement speed, direction, and delta time.
    */
    private void MoveBall()
      {

        // Declare and initialize local variables, specific to the MoveBall method scope.
        Vector3 displacementFromStart = Vector3.zero;
        float distanceTraveled = 0;

        // Calculate the distance traveled since the original position.
         displacementFromStart = transform.position - m_originalStartingPosition;
         distanceTraveled = displacementFromStart.magnitude;

        // Constraint: Check if the ball has traveled the specified distance to change direction.
        if(distanceTraveled >= m_distanceUntilDirectionChange)
        { 
          // If so, reverse the movement direction.
          m_movementDirection = -m_movementDirection;
        }

        // Translate the ball position based on movement speed, direction, and delta time.
        transform.Translate(m_movementDirection * m_movementSpeed * Time.deltaTime, Space.World);
        

    }


    public IEnumerator RotateBallRoutine()
    {
      // Declare and initialize variables.
      m_originalStartingRotation = transform.rotation; // Store the original starting rotation of the ball for future reference.

      // Infinite loop to keep the coroutine running indefinitely.
      while(true)
      {
        RotateBall(); // Call the RotateBall method to update the ball's rotation.
        yield return null; // Yield control back to the main loop until the next frame, allowing other processes to run.
      }

    }

    /*
      Name: RotateBall
      Input: None
      Ouput: None
      Dependencies: MonoBehaviour
      Process: 
               1.) Get the ball's original rotation in Start().
               2.) Calculate the angle difference from the original rotation.
               3.) Constraints: Check if the ball has rotated the specified amount to change rotation direction.
               4.) Rotates the ball's transform based on rotation axis, speed, and delta time.
    */
    private void RotateBall()
      {
          // Declare and initialize local variables, specific to the RotateBall method scope.
          Quaternion rotatedAmountFromOriginalRotation = Quaternion.identity;
          float angleDifference = 0f; 

         // Calculate the angle difference from the original rotation.
         rotatedAmountFromOriginalRotation = Quaternion.Inverse(m_originalStartingRotation) * transform.rotation;
         
         // Quaternion.Angle returns a value between 0 and 180 degrees.
         angleDifference = Quaternion.Angle(m_originalStartingRotation, rotatedAmountFromOriginalRotation);
      
        // Constraints: Check if the ball has rotated the specified amount to change rotation direction.
        if(angleDifference >= m_rotationUntilDirectionChange)
        {
      
          // If so, reverse the rotation direction.
          m_rotationSpeed = -m_rotationSpeed;

        }

        // Rotate the ball's transform based on rotation axis, speed, and delta time.
        transform.Rotate(m_rotationAxis, m_rotationSpeed * Time.deltaTime);

      }

      public IEnumerator ScaleBallRoutine()
      {

        // Declare and initialize variables.

        // loop to keep the coroutine running indefinitely.
        while(true)
        {
          ScaleBall(); // Call the ScaleBall method to update the ball's scale.

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

        // Constraints: Ensure the ball's scale remains within the specified min and max limits.
         if(transform.localScale.x >= m_maxBallScale || transform.localScale.x <= m_minBallScale)
         {
            // Reverse scaling direction if limits are reached.
              m_scalingDirection = -m_scalingDirection;
         }


        // Scale the ball up and down between min and max scale using a function over time.
        transform.localScale += Vector3.one * m_scalingDirection * m_ballScalingSpeed * Time.deltaTime;


    }

  }
}
