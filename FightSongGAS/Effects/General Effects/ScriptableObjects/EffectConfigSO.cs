using FightSongSoundSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Effect Configuration", menuName = "Scriptable Objects/EffectConfiguration")]
public class EffectConfigSO : ScriptableObject 
{


  // [SerializeField] SoundConfigSO m_SoundConfigSO
  // [SerializeField] List<SoundConfigSO> m_Sounds
  [SerializeField] List<SoundConfigSO> m_Sounds;

  [SerializeField] Transform m_VFXTransformOnPlay;
  [SerializeField] Vector3 m_VFXScale = new();
  [SerializeField] GameObject m_GameObjectPoolPrefab;

  public List<SoundConfigSO> GetSounds()
  { 
     return m_Sounds; 
  }

  public GameObject GetGameObjectPoolPrefab()
  {
     return m_GameObjectPoolPrefab;  
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
