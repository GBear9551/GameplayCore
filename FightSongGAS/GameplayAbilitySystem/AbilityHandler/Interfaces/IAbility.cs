using NUnit.Framework;
using UnityEditor.UI;
using UnityEngine;
using System.Collections.Generic;

namespace FightSongGameLogicSystem 
{
    public interface IAbility
    {
        //List<ITargetable> m_Targets;
        List<IModifier> Use(GameObject gameObj, List<GameObject> targets);
        bool AbilitySOConfigCheck<T>();
    }
}
