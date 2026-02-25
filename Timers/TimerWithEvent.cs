using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class TimerWithEvent : MonoBehaviour
{

  [SerializeField] bool m_IsTimerOnRepeat = false;
  [SerializeField] float m_TimerInSeconds = 5f;

  public UnityEvent OnTimerComplete;

  private void Start()
  {
    StartCoroutine(StartTimerRoutine());
  }

  private IEnumerator StartTimerRoutine()
  {
     var timeToWait = new WaitForSeconds(m_TimerInSeconds);

     do
     {
       yield return timeToWait;
       Debug.Log("Time invoked: "+ Time.time);

       OnTimerComplete?.Invoke();
 
     } while(m_IsTimerOnRepeat);




  }

}
