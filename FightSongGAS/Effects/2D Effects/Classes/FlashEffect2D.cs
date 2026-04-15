using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
  public class FlashEffect2D : Effect2D
  {

    [SerializeField] SpriteRenderer[] m_SpriteRenderers;
    [SerializeField] float m_FlashEffectDuration;
    [SerializeField] Color m_ColorToFlash;


    private Color[] m_OrginialColors ;
    private bool m_FlashEffectEnabled = false;
    private float m_FlashTimer;

    private void OnEnable()
    {
      int index = 0;
      m_OrginialColors = new Color[m_SpriteRenderers.Length];
      
      foreach(var renderer in m_SpriteRenderers)
      {
         m_OrginialColors[index] = renderer.color;
         index++;
      }
    }

    private void OnDisable()
    {
      RevertFlashColor();
    }

    private void Update()
    {
      if(m_FlashEffectEnabled)
      {
        m_FlashTimer += Time.deltaTime;

        if(m_FlashTimer > m_FlashEffectDuration)
        {
          m_FlashTimer = 0f;
          RevertFlashColor();
        }
      }
 
    }

    public override GameObjectPool PlayVFX()
    {

      //GameObjectPool vfxGameObjectPool = base.PlayVFX();
      // Check to see if the flash effect is already in progress.
      if(!m_FlashEffectEnabled) 
      {

        // Check for 2d Gameplay color
        var gameplay2DColor = GetComponent<Gameplay2DColor>();
        if(gameplay2DColor != null)
        {
           SaveOriginalColors();
        }

        m_FlashEffectEnabled = true;
        FlashColor();
      }


      return null; // vfxGameObjectPool; 

    }

    private void SaveOriginalColors()
    {
      int index = 0;

      foreach (var renderer in m_SpriteRenderers)
      {
        m_OrginialColors[index] = renderer.color;
        index++;
      }
    }

    private void FlashColor()
    {

       // We must have sprite renderers to flash.
       if(m_SpriteRenderers == null)
        return;

      // Change the color of the sprite renderers
      foreach( var renderer in m_SpriteRenderers)
      {
        renderer.color = m_ColorToFlash;
      }

    }

    private void RevertFlashColor()
    {

      int index = 0;
      m_FlashEffectEnabled = false;

      // We must have sprite renderers to flash.
      if (m_SpriteRenderers == null)
        return;

      // Change the color of the sprite renderers
      foreach (var renderer in m_SpriteRenderers)
      {
        renderer.color = m_OrginialColors[index];
        index++;
      }
    }

  }
}
