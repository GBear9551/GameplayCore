using NUnit.Framework;
using UnityEditor.Animations;
using UnityEngine;
using System.Collections.Generic;
namespace FightSongGameLogicSystem
{
    public interface IModifier
    {
       bool Apply();
       bool Remove();
       bool Refresh();
       GameObject GetFrom();
       bool SetFrom(GameObject from);
       bool SetTargets(List<GameObject> targets);
       void Initialize(GameObject from, List<GameObject> targets, float duration);
    }
}
