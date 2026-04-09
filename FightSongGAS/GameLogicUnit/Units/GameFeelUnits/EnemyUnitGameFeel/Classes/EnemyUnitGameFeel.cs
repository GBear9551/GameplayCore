using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FightSongGameLogicSystem;

public class EnemyUnitGameFeel : Unit, IUnitBrainMotion 
{


    public float GetChangeDirectionInterval()
    {
       return m_UnitConfig.GetChangeDirectionInterval();
    }
}
