using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Effect Configuration", menuName = "Scriptable Objects/EffectConfiguration")]
public class EffectConfigSO : ScriptableObject 
{

  [SerializeField] List<AudioClip> m_AudioClipList = new();
  [SerializeField] AudioSource m_AudioSource = new();
  [SerializeField] Transform m_VFXTransformOnPlay;
  [SerializeField] Vector3 m_VFXScale = new();
  [SerializeField] GameObject m_GameObjectPoolPrefab;

  public GameObject GetGameObjectPoolPrefab()
  {
     return m_GameObjectPoolPrefab;  
  }
 
  public List<AudioClip> GetAudioClipList()
  {
    return m_AudioClipList;
  }

  public AudioSource GetAudioSource()
  { 
    return m_AudioSource; 
  }

  public Transform GetTransformOnPlay()
  {
    return m_VFXTransformOnPlay;
  }

  public Vector3 GetVFXScale()
  { 
    return m_VFXScale; 
  }

}
