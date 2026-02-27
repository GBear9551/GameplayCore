using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace GameDevTV
{
    public class CoroutineManager : MonoBehaviour
    {
  
      //[SerializeField] private CoroutineManagerConfig m_CoroutineManagerConfig;
      [SerializeField] private int m_MaxNumOfCoroutines;
      [SerializeField] private List<Coroutine> m_Coroutines;

   
      public bool Init(int maxNumOfCoroutines)
      {
         m_MaxNumOfCoroutines = maxNumOfCoroutines;
         return true;
      }

      public bool AddCoroutine(Coroutine coroutine)
      {
        if(m_Coroutines == null)
        {
            m_Coroutines = new List<Coroutine>();
        }

        if(m_Coroutines.Count < m_MaxNumOfCoroutines)
        {
            m_Coroutines.Add(coroutine);
            return true;
        }
        else
        {
            Debug.LogError("Max num of coroutines reached, cannot add more coroutines to manager.");
            return false;
        }
    }

    public bool StopManagedCoroutine(Coroutine coroutine)
    {
       if(m_Coroutines != null)
       {

         if(m_Coroutines.Contains(coroutine))
         {
            StopCoroutine(coroutine);
            return true;
         }
         else
         {
            Debug.LogError("Coroutine not found in manager, cannot stop coroutine.");
            return false;
         }

       }

        return false;
    }

    public bool RemoveManagedCoroutine(Coroutine coroutine)
    {
      if (m_Coroutines != null)
      {

        if (m_Coroutines.Contains(coroutine))
        {
          StopCoroutine(coroutine);
          m_Coroutines.Remove(coroutine);
          return true;
        }
        else
        {
          Debug.LogError("Coroutine not found in manager, cannot stop coroutine.");
          return false;
        }

      }
       return false;
    }
    

      public int GetMaxNumOfCoroutines()
      {
        return m_MaxNumOfCoroutines; 
      }

      public bool SetMaxNumOfCoroutines(int value)
      {
         if(value < m_MaxNumOfCoroutines)
         { 
            Debug.LogError("Max num of coroutines must be greater than previous value, to prevent truncated coroutine management.");   
            return false;
         }
         else
         {
              m_MaxNumOfCoroutines = value;
              return true;
         }   
      }

      public List<Coroutine> GetCoroutines() 
      { 
        return m_Coroutines; 
      }

      public bool SetCoroutines(List<Coroutine> coroutines)
      {

        if(coroutines != null)
        {
            if(m_Coroutines != null)
            {
              foreach(var coroutine in m_Coroutines)
              {
 
                 StopCoroutine(coroutine);

              }
            }
            m_Coroutines = coroutines;
            return true;
         
        }
        else
        {
            Debug.LogError("Coroutines list cannot be null, to prevent truncated coroutine management.");
            return false;
        }
    }

    public bool IsFull()
    {
      if(m_Coroutines != null)
      {
        if(m_Coroutines.Count < m_MaxNumOfCoroutines)
        {
          return false;
        }
        else
        {
          return true;
        }
      }
      else
      {
        return false;
      }
    }

    public bool Clear()
    {
      if (m_Coroutines != null)
      {
        foreach (var coroutine in m_Coroutines)
        {
          RemoveManagedCoroutine(coroutine);
        }
        m_Coroutines = null;
        return true;
      }
      else
      {
        return false;
      }
    }


  }
}
