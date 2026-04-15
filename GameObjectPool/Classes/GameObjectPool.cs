using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using System.Collections.Generic;
using FightSongLoggingSystem;
using System.Linq;

public class GameObjectPool : MonoBehaviour
    {

       // Possibly extend to return a randomly obtained prefab from list, or and entire list created prefabs. 

       public List<GameObject> m_prefabsToPool;
       public Transform m_ParentObjectForPooledObject;
       private int m_numOfActiveObjects = 0;
       private int m_currentListIndex = 0;
       
       public IObjectPool<GameObject> Pool;
       public UnityEvent<GameObject> OnCreateDependencyInjectionEvent;

       [SerializeField] private bool m_collectionCheck = true;
       [SerializeField] private int m_defaultCapacity = 10;
       [SerializeField] private int m_maxSize = 10;
       private List<GameObject> m_ActiveObjectsInPool = new List<GameObject>();
       private int m_InstanceCreationAttempts = 0;


       private static List<GameObjectPool> m_RegisteredPools = new List<GameObjectPool>();
       private GameObject m_GameObjectPoolPrefabIdentity;

    public static GameObjectPool FindOrCreateAndRegisterPool(GameObject prefabPool)
    {
        // Declare and initialize variables
          var existingPool = m_RegisteredPools.FirstOrDefault(pool =>
              pool != null && pool.m_GameObjectPoolPrefabIdentity == prefabPool);

          bool poolExists = existingPool != null;

        // Check to make sure the game object pool prefab has a game object pool component.
          var gameObjectPoolComp = prefabPool.GetComponent<GameObjectPool>();
     
        // Check to see the pool has been created already, an object instance of the gopool exists, then return that one.
          if (!poolExists)
          { 
            if(gameObjectPoolComp != null )
            {
               var newGameObjectPoolObject = Instantiate(prefabPool);
               var newGameObjectPoolComponent = newGameObjectPoolObject.GetComponent<GameObjectPool>();

               newGameObjectPoolComponent.m_GameObjectPoolPrefabIdentity = prefabPool;

               m_RegisteredPools.Add(newGameObjectPoolComponent);
               return newGameObjectPoolComponent;
            }
          }
       
          return existingPool;

    }

    

    private void Awake()
    {
        Pool = new ObjectPool<GameObject>(
               OnCreate,
               OnGet,
               OnRelease,
               OnDestroyPooledGameObj,
               m_collectionCheck,
               m_defaultCapacity,
               m_maxSize
               );
    }

    public void Initialize()
    {
           Pool = new ObjectPool<GameObject>(
           OnCreate,
           OnGet,
           OnRelease,
           OnDestroyPooledGameObj,
           m_collectionCheck,
           m_defaultCapacity,
           m_maxSize
           );
  }
  

    public GameObject OnCreate()
    {

      GameObject gameObj = null;

      if(m_numOfActiveObjects >= m_maxSize)
      {
        LoggingSystem.LogString($"[DEBUG[ {this.gameObject.name} Max pool size reached! Consider increasing the max size or reducing the spawn rate.",Unity.VisualScripting.WarningLevel.Info);
        return null;
      }

      if (m_prefabsToPool != null)
      {
        m_currentListIndex = Random.Range(0, m_prefabsToPool.Count);

        if (m_ParentObjectForPooledObject == null)
        {
          gameObj = Instantiate(m_prefabsToPool[m_currentListIndex]);
        }
        else
        {
         gameObj = Instantiate(m_prefabsToPool[m_currentListIndex], m_ParentObjectForPooledObject);
        }



        gameObj.GetComponent<IPooledGameObject>().SetPool(Pool);

        if(OnCreateDependencyInjectionEvent != null)
        {
           OnCreateDependencyInjectionEvent.Invoke(gameObj);
        } // Invoke the event to initialize any dependencies for the newly created GameObject.
        return gameObj;
      }
      else
      {
        Debug.LogError("No prefabs assigned to pool!");
        return null;
      }
 
    }

    public void OnGet(GameObject gameObject)
    {
      if (gameObject == null)
        return;

      if (m_ActiveObjectsInPool.Contains(gameObject))
      {
        LoggingSystem.LogString($"OnGet duplicate active object: {gameObject.name}",Unity.VisualScripting.WarningLevel.Caution);
        return;
      }

      gameObject.SetActive(true);
      m_ActiveObjectsInPool.Add(gameObject);

      LoggingSystem.LogString($"GET: {gameObject.name}, ActiveCount = {m_ActiveObjectsInPool.Count}", Unity.VisualScripting.WarningLevel.Info);
      m_numOfActiveObjects++;
  }


    public void OnRelease(GameObject obj)
    {
      if (obj == null)
        return;

      if (!m_ActiveObjectsInPool.Remove(obj))
      {
        LoggingSystem.LogString($"OnRelease missing active object: {obj.name}", Unity.VisualScripting.WarningLevel.Caution);
      }

      obj.SetActive(false);

      LoggingSystem.LogString($"RELEASE: {obj.name}, ActiveCount = {m_ActiveObjectsInPool.Count}", Unity.VisualScripting.WarningLevel.Info);
      m_numOfActiveObjects--;
  }

    public void OnDestroyPooledGameObj(GameObject gameObject)
    {
      DestroyImmediate(gameObject);
    }

    public List<GameObject> GetActiveObjectsInPool()
    {
       return m_ActiveObjectsInPool;
    }

}
