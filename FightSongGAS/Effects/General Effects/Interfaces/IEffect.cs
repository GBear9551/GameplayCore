using UnityEngine;

namespace FightSongGameLogicSystem
{
    public interface IEffect
    {

        GameObjectPool CreateVFXGameObjects();
        GameObjectPool PlayVFX();
        void PlaySFX();
    }
}
