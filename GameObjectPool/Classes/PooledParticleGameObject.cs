using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledParticleGameObject : PooledGameObject
{

  private ParticleSystem m_particleSystem;
  private Coroutine m_returnRoutine;

  private void Awake()
  {
    m_particleSystem = GetComponent<ParticleSystem>();
  }

  private void OnDisable()
  {
    m_returnRoutine = null;
  }

  public void PlayPooledParticle()
  {
    var ps = GetComponent<ParticleSystem>();

    if (ps == null)
      return;

    ps.Play(true);

    ReturnWhenFinished();
  }

  private void ReturnWhenFinished()
  {
    if (m_returnRoutine != null)
    {
      StopCoroutine(m_returnRoutine);
    }

    m_returnRoutine = StartCoroutine(ReturnWhenFinishedRoutine());
  }
  private IEnumerator ReturnWhenFinishedRoutine()
  {
    if (m_particleSystem == null)
    {
      Debug.LogError($"No ParticleSystem found on {gameObject.name}");
      yield break;
    }

    yield return new WaitUntil(() => !m_particleSystem.IsAlive(true));

    base.ReturnToPool();

  }



}
