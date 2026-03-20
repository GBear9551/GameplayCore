using UnityEngine;

namespace FightSongGameLogicSystem
{
  public class ScaleModifier : Modifier
  {


    [SerializeField] Vector3 m_ScalingAmount;


    public ScaleModifier(Vector3 scalingAmount)
    {
      m_ScalingAmount = scalingAmount;
    }

    public Vector3 GetModifier()
    {
      return m_ScalingAmount;
    }

    public override bool Remove()
    {
      bool removeFromTargets = base.Remove();

      // Reverse Scaling by scaling amount 

      return removeFromTargets;

    }


  }
}
