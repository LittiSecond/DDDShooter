using System;
using System.Collections.Generic;
using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class NpcCommander : BaseController, IExecute, IInitialization
    {
        #region Fields

        private List<EnemyBaseLogic> _botList = new List<EnemyBaseLogic>();

        #endregion
        

        #region Methods

        private void RemoveBot(EnemyBaseLogic logic)
        {
            _botList.Remove(logic);
            ServiceLocator.Resolve<MiniMapController>().RemoveObject(logic.Transform);
        }

        #endregion
        

        #region IExecute

        public void Execute()
        {
            for (int i = 0; i < _botList.Count; i++)
            {
                _botList[i].Execute();
            }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            MiniMapController miniMap = ServiceLocator.Resolve<MiniMapController>();

            EnemyBody[] bodies = GameObject.FindObjectsOfType<EnemyBody>();
            foreach (EnemyBody body in bodies)
            {
                NpcSettings npcSettings = body.Settings;
                EnemyBaseLogic logic = null;
                switch (npcSettings.Type)
                {
                    case NpcType.RangeAttack: 
                        logic = new EnemyLogicRangeAttacker(body);
                        break;
                    case NpcType.StupidRunner:
                        logic = new EnemyLogickStupidRunner(body);
                        break;
                    default:
                        break;
                }

                if (logic != null)
                {
                    _botList.Add(logic);
                    logic.OnDestroyEventHandler += RemoveBot;
                    miniMap.AddObject(body.Transform);
                }
            }
        }

        #endregion
    }
}
