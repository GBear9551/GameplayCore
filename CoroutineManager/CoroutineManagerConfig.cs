using System.Collections.Generic;
using UnityEngine;

namespace GameDevTV
{
    [System.Serializable]
    public struct CoroutineManagerConfig 
    {
      [SerializeField] public int m_MaxNumOfCoroutines;
      [SerializeField] public List<Coroutine> m_Coroutines;

    }
}
