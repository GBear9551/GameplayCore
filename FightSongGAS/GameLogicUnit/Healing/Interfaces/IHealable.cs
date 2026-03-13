using UnityEngine;

using HealthType = System.Int32;

namespace GameDevTV
{
    public interface IHealable  
    {

       HealthType Heal(HealthType amountToHealBy, GameObject healingSource);

    }
}
