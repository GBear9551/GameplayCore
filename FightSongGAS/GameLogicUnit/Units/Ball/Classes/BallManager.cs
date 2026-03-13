using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
namespace GameDevTV
{ 

    // Many Unit->Ball Application, acts on many ball objects from a ball object pool. 
    // GameUnitSelection, SelectionGroup,
    // Goal: Perform operation on all active members from a pool. Custom UnityEvent for testing collection response, gameplay, and gameObjPool responses.
  

    public class BallManager : MonoBehaviour
    {

       
        public UnityEvent<GameObject> OnApplyFunctionToAllBalls;
        [SerializeField] private GameObjectPool m_Balls;
        private List<GameObject> Balls = new List<GameObject>();

        private void Start()
        {
          if(m_Balls == null)
          {
            Debug.LogError("BallManager reference to GameObjectPool is not set!");
          }
        }

        public void PostMessage()
        {
            Debug.Log("Message from BallManager!");
        }

        public void FillBallList(List<GameObject> gameObjects)
        {
            Balls = gameObjects;
        }

        public void AddBall(GameObject ball)
        {
            Balls.Add(ball);
        }

        public void RemoveBall(GameObject ball)
        {
            Balls.Remove(ball);
        }

        public void ApplyFunctionToAllBalls()
        {

          FillBallList(m_Balls.GetActiveObjectsInPool()); // Get the list of active balls from the pool and fill the class list.
    
          foreach (GameObject ball in Balls)
          {
             if(ball.activeInHierarchy)
             {
               OnApplyFunctionToAllBalls?.Invoke(ball);
             }
          }
        }

  }
}
