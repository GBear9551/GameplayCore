using UnityEngine;
using UnityEngine.Events;
using FightSongGameLogicSystem;

public class BallOnSpawn : MonoBehaviour
{

    [Header("Ball On Spawn Settings")]
    [SerializeField] BallOnSpawnConfig m_OnSpawnConfig;

    public UnityEvent<GameObject> OnStartSpawn;
    public KeyboardMovement KeyboardMovement;
    public KeyboardControlInput Input;

    private void Start()
    {
      OnStartSpawn?.Invoke(this.gameObject);
    }

  public void OnSpawn(GameObject ball)
    {
 
       // Declare and initialize variables
        Color color = Color.white; // Default color for the ball, will be updated if the ball has a GameplayColor component.

       if (ball != null)
       { 

         // Reset ball, because the ball release has coroutines that effect its scale, and other states ended. 
         // This ensures that the ball is reset to its default state when it is spawned from the pool. 
           //ball.GetComponent<ResetBall>().ResetBall(); // Reset the ball's state using the ResetBall method in the BallGameRules script, which resets the ball's scale, velocity, and other relevant states to their default values.

         // Get the color of the ball
           color = GetBallColor(ball); // Get the color of the ball using the GetBallColor method, which checks for a GameplayColor component on the ball and retrieves the color from it. If the component is not found, it returns a default color (white).

         // Play on Spawn SFX.
          //PlaySpawnSFX();

         // Play On Spawn VFX, using the ball vfx handler, pass the position of the ball and the default color for the trail. 
          PlaySpawnVFX(ball, color);

         // Apply transform modifications
           //ApplyTransformModifications();

         // Apply Initiali force to the ball in the specified direction and speed when it is spawned from the pool.
           ApplyInitialForce(ball);

       }
    }


    public void AddPlayerKeyboardControl(GameObject ball)
    {
       if(!ball.TryGetComponent<KeyboardControlInput>(out KeyboardControlInput keyboardInput))
       {
          keyboardInput = ball.AddComponent<KeyboardControlInput>();

          var keyboardMovement = ball.GetComponent<KeyboardMovement>();

          if(keyboardMovement == null)
          {
             keyboardMovement = ball.AddComponent<KeyboardMovement>();
          }

          BallUnit ballUnit = ball.GetComponent<BallUnit>();
          if (ballUnit != null) 
          {
            keyboardInput.Init(keyboardMovement, ballUnit, Input.MoveVertically, Input.MoveHorizontally); 
          }
          else 
          {
             Debug.LogError("Prefab must can the component type BallUnit. Class:BallOnSpawn, Function: AddPlayerKeyboardControl(...)");
          }  
       }
    }


    public void RandomizeColor(GameObject ball)
    {
        int randomColorIndex = 0;
        int possibleColorsCount = m_OnSpawnConfig.GetPossibleBallColorsCount(); // Get the count of possible ball colors from the BallOnSpawnConfig, which is used to determine the range for generating a random index for selecting a color from the list of possible ball colors.
        ColorSO randomColorSO = null;

        if (ball != null && possibleColorsCount > 0)
        {
           var ballGameplayColor = ball.GetComponent<GameplayColor>();

           if (ballGameplayColor != null) 
           {
               
               randomColorIndex = Random.Range(0, possibleColorsCount); // Get a random index for the possible ball colors list.
               randomColorSO = m_OnSpawnConfig.GetPossibleBallColorAtIndex(randomColorIndex); // Get a random color from the list of possible ball colors using the random index.
               ballGameplayColor.SetColor(randomColorSO);


               // Change the ball's trail color

               // Get the ball's current color
               var ballCurrentColor = GetBallColor(ball);

               /*var ballTrailColorHandler = ball.GetComponent<BallTrailColorHandler>();
               if (ballTrailColorHandler != null)
               {
                  //Color colorComplement = BallTrailColorHandler.GetComplementaryColor(ballCurrentColor);
                  ballTrailColorHandler.ChangeBallTrailColor(ballCurrentColor, ballCurrentColor);// colorComplement);
                  //ballTrailColorHandler.ChangeBallOuterTrailColor(ballCurrentColor);
               }*/
               
           }
        }
    }
    private void PlaySpawnVFX(GameObject ball, Color color)
    {
      /*var ballVFXRequestor = ball.GetComponent<BallVFXRequestor>();
      if (ballVFXRequestor != null)
      { 
        ballVFXRequestor.RequestSpawnAnimation(color); // Request the spawn VFX at the ball's position with the default color for the trail.
      }*/
  }

  private void ApplyInitialForce(GameObject ball)
    {
        var rb = ball.GetComponent<Rigidbody2D>();
        if(rb != null)
        { 
          rb.linearVelocity = m_OnSpawnConfig.GetLinear2DRigidBodyVelocity(); // Apply the initial velocity to the ball's Rigidbody2D component based on the direction and speed specified in the BallOnSpawnConfig. This sets the initial movement of the ball when it is spawned from the pool.
          Debug.Log("Linear velocity applied to ball on spawn: " + rb.linearVelocity);
        }
    }

  private Color GetBallColor(GameObject ball)
  {
    var ballGameplayColor = ball.GetComponent<GameplayColor>();
    if (ballGameplayColor != null) 
    {
      return ballGameplayColor.m_colorSO.color; // Get the color from the ball's GameplayColor component.
    }
    return Color.white; // Return a default color if the GameplayColor component is not found.
  }

}
