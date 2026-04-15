using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FightSongGameLogicSystem
{

  [CreateAssetMenu(fileName = "Color Container Configuration", menuName = "Scriptable Objects/Color/ColorContainerConfiguration")]
  public class ColorContainerConfigSO : ScriptableObject
  {

    [SerializeField] Color[] m_Colors;

    public Color[] GetColors()
    {

      return m_Colors;

    }



  }
}
