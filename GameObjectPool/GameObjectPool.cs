using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using System.Collections.Generic;

    public class GameObjectPool : MonoBehaviour
    {

       public List<GameObject> m_prefabsToPool;
       private int m_numOfActiveObjects = 0;
       private int m_currentListIndex = 0;
       
       public IObjectPool<GameObject> Pool;
       public UnityEvent<GameObject> OnCreateDependencyInjectionEvent;

       [SerializeField] private bool m_collectionCheck = true;
       [SerializeField] private int m_defaultCapacity = 10;
       [SerializeField] private int m_maxSize = 10;
       private List<GameObject> m_ActiveObjectsInPool = new List<GameObject>();

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
  

    public GameObject OnCreate()
    {

      if(m_numOfActiveObjects >= m_maxSize)
      {
        Debug.LogWarning("Max pool size reached! Consider increasing the max size or reducing the spawn rate.");
        return null;
      }

      if (m_prefabsToPool != null)
      {
        m_currentListIndex = Random.Range(0, m_prefabsToPool.Count);
        var gameObj = Instantiate(m_prefabsToPool[m_currentListIndex]);
        gameObj.GetComponent<PooledGameObject>().SetPool(Pool);
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
      m_numOfActiveObjects++;
      if (gameObject != null)
      {
        gameObject.SetActive(true);
        m_ActiveObjectsInPool.Add(gameObject);
      }
    }

    public void OnRelease(GameObject obj)
    {
      m_numOfActiveObjects--;
      obj.SetActive(false);
      m_ActiveObjectsInPool.Remove(obj);
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
