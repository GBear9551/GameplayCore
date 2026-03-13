using UnityEngine;
using UnityEngine.InputSystem;

namespace FightSongGameLogicSystem
{
    public class KeyboardControlInput : PlayerInput
    {

        [SerializeField] private KeyboardMovement m_Movement;
        private Unit m_Unit;
        public InputAction MoveVertically;
        public InputAction MoveHorizontally;



        public void OnEnable()
        {
           MoveVertically?.Enable();
           MoveHorizontally?.Enable();
           
        }

        public void Init(KeyboardMovement movement, Unit unit, InputAction moveVert, InputAction moveHorizontal)
        {
          m_Movement = movement;
          m_Unit = unit;
          MoveHorizontally = moveHorizontal;
          MoveVertically = moveVert;
          MoveVertically?.Enable();
          MoveHorizontally?.Enable();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
           Debug.Log("Keyboard Control Input on GameObject: " + gameObject.name);
           m_Unit = GetComponent<Unit>();
        }

        // Update is called once per frame
        void Update()
        {

           float verticalMotion = 0f;
           float horizontalMotion = 0f;
           float speed = 0f;

           // Handle Vertical Motion
           if(MoveVertically != null) 
           {
              verticalMotion = MoveVertically.ReadValue<float>();
           }
       
           // Handle Horizontal Motion
           if(MoveHorizontally != null) 
           {
              horizontalMotion = MoveHorizontally.ReadValue<float>();
           }
           if( horizontalMotion != 0 ||  verticalMotion != 0 ) 
           {
             var movementDirection = new Vector2(horizontalMotion, verticalMotion);
             if(m_Movement != null) 
             {
               speed = m_Unit.GetSpeed();
               m_Movement.Move(movementDirection, speed);
             }
            }
        
        }
    }
}
