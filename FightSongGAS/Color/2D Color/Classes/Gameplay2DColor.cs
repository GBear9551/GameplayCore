using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace FightSongGameLogicSystem
{
  public class Gameplay2DColor : MonoBehaviour
  {

    [SerializeField] public Color Color;

    [SerializeField] private SpriteRenderer[] m_SpriteRenderers;
    [SerializeField] private ColorContainerConfigSO m_RandomSelectableColors;


    public UnityEvent OnStartEvent;


    public static bool SetParticleSystemColor(GameObject vfx, Color color)
    {
       ParticleSystem particleSystem = vfx.GetComponent<ParticleSystem>();
 
       if (particleSystem != null)
       {
            var mainModule = particleSystem.main;
            mainModule.startColor = color;
            return true;
      }
      return false;
    }

    private void Awake()
    {
      OnStartEvent?.Invoke();
    }

    private void OnEnable()
    {
      //SetColor(Color);
    }

    public void SetGameObjectSpawnedColor(GameObject gameObj)
    {
      var color = gameObj.GetComponent<Gameplay2DColor>();

      if (color != null)
      {
        color.SetColor(Color);
      }
    }

    public void SetColor(Color color)
    {

      

      foreach (var renderer in m_SpriteRenderers)
      {
        renderer.color = color;
        Color = color;


      }
    }

    public void RandomizeColor()
    {
      Color[] colors = m_RandomSelectableColors.GetColors();
      if (colors == null) return;
      int randIndex = Random.Range(0, colors.Length);
      Color randomColor = colors[randIndex];

      foreach (var renderer in m_SpriteRenderers)
      {
        renderer.color = randomColor;
        Color = randomColor;

      }
    }

  }
}
