using UnityEngine;
using UnityEngine.Pool;

    public class PooledGameObject : MonoBehaviour, IPooledGameObject
    {

       protected IObjectPool<GameObject> m_pool;

       public virtual void SetPool(IObjectPool<GameObject> pool)
       {
           m_pool = pool;
       }

       public virtual void ReturnToPool()
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
