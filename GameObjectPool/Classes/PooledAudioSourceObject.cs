using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledAudioSourceObject : PooledGameObject
{

  private AudioSource m_AudioSource;

  private void Awake()
  {
    m_AudioSource = GetComponent<AudioSource>();
  }

  public virtual void Play()
  {
 
    // If the audio source is set to play on awake, then isPlaying will be true
    // once the pooled audio source object is obtained from the game object pool.
    // Set AudioSources on gameobjects that are pooled to not play on awake in order to
    // avoid a dirty life cycle bug with pooled audio source objects. 

    if (m_AudioSource != null && !m_AudioSource.isPlaying)
    {
      m_AudioSource.Play();
      if (!m_AudioSource.loop)
      {
        StartCoroutine(ReturnToPoolRoutine());
      }
    }
  }

  private IEnumerator ReturnToPoolRoutine()
  {
     var waiter = new WaitForSeconds( m_AudioSource.clip.length);
     yield return waiter;
     m_pool.Release(this.gameObject);
  }
}
