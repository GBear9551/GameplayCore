using UnityEngine;

using DamageType = System.Int32;

namespace FightSongGameLogicSystem
{
        

    public interface IDamageable 
    {
       // Always public when using interfaces.
       DamageType TakeDamage(DamageType damageToApply, GameObject damageSource);
    }
}
