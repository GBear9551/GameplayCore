using UnityEngine;
using UnityEngine.Pool;

    public class PooledGameObject : MonoBehaviour
    {

       private IObjectPool<GameObject> m_pool;

       public void SetPool(IObjectPool<GameObject> pool)
       {
           m_pool = pool;
       }

       public void ReturnToPool()
       {
          if (m_pool != null)
          {
               if (gameObject.activeInHierarchy)
               {
                // Check for coroutines
                StopAllCoroutines();
                m_pool.Release(this.gameObject);
               }
          }

          else
          {
              Debug.LogError("Pool reference is null! Cannot return to pool.");
          }

    }

  }
