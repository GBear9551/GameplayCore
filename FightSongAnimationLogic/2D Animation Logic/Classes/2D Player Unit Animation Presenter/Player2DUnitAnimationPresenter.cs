using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightSongAnimationSystem
{
  public class Player2DUnitAnimationPresenter : Unit2DAnimationPresenter
  {


 
    

    protected override void PlayJumpEffect()
    {

      base.PlayJumpEffect();
    }

    protected override void PlayWalkEffect()
    {

       // Get facing direction

       // Get motion direction

       // Apply lean based on facing and motion direction vectors.


       base.PlayWalkEffect();
    }

  }
}
