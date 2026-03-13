using NUnit.Framework;
using UnityEditor.Animations;
using UnityEngine;

namespace FightSongGameLogicSystem
{
    public interface IModifier
    {
       void Apply();
       void Remove();
    }
}
