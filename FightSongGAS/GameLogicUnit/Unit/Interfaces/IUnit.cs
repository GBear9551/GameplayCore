using GameDevTV;
using UnityEngine;


// Collects the identity of the Unit and contractability of abstract Unit
namespace FightSongGameLogicSystem 
{
    public interface IUnit : IDamageable, IHealable, ITargetable, IMoveable, IModifiable
    {

    }
}
