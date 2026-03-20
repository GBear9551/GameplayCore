using UnityEngine;

using HealthType = System.Int32;

namespace FightSongGameLogicSystem 
{
    public interface IHealable  
    {

       HealthType Heal(HealthType amountToHealBy, GameObject healingSource);

    }
}
