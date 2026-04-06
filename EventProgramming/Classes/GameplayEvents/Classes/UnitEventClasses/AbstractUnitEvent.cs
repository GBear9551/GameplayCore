using UnityEngine;
using FightSongGameLogicSystem;

namespace FightSongEventProgrammingSystem
{
    public abstract class AbstractUnitEvent : IUnitEvent
    {

      public Unit m_UnitDataModel;

      public virtual Unit GetUnitDataModel()
      {
         return m_UnitDataModel;
      }

      

    }
}
