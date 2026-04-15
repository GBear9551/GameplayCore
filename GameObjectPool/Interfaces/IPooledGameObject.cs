using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IPooledGameObject 
{
  public void SetPool(IObjectPool<GameObject> pool);
  public void ReturnToPool();
}
