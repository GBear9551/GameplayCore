using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongGameLogicSystem
{
  public class SplatterDeathEffect2D : Effect2D
  {

     [SerializeField] GameObject m_SplatterPrefab;
      Transform m_SplatterImageSortingHandler;

     

     public override GameObjectPool PlayVFX()
     {

        GameObject splatter = null;
        m_SplatterImageSortingHandler = transform.parent; 

        //GameObjectPool vfxObjectsCreated = base.PlayVFX();
        GameObjectPool vfxPool = base.CreateVFXGameObjects();
        GameObject vfx = null;

        // Get a vfx from the pool
        if( vfxPool != null )
        {
            vfx  = vfxPool.Pool.Get();
        }


        // Get color of this entity
        var gameplay2DColor = GetComponent<Gameplay2DColor>();
        Color ourDieingGameObjectColor = gameplay2DColor.Color;

        // Set the color of Particle System's main module
        if (vfx != null)
        {
          vfx.transform.position = transform.position;
          Gameplay2DColor.SetParticleSystemColor(vfx, ourDieingGameObjectColor);
          var ps = vfx.GetComponent<PooledParticleGameObject>();
          ps.PlayPooledParticle();
        }

        // spawn the splatter
        if (m_SplatterPrefab != null)
        {
           splatter = Instantiate(m_SplatterPrefab, transform.position, Quaternion.identity, m_SplatterImageSortingHandler);
        }
        // paint our splatter
        if (splatter != null)
        {
          var splatterGameplay2DColor = splatter.GetComponent<Gameplay2DColor>();
          splatterGameplay2DColor.SetColor(ourDieingGameObjectColor);
        }
        
        
        return vfxPool;
     }

  }
}
