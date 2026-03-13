using NUnit.Framework;
using UnityEditor.Animations;
using UnityEngine;

namespace FightSongGameLogicSystem
{
    public interface IModifier
    {
       bool Apply();
       bool Remove();
    }
}
