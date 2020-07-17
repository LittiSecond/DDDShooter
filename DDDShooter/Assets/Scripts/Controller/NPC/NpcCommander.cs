using System;
using System.Collections.Generic;
using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class NpcCommander : BaseController, IExecute, IInitialization
    {
        #region Fields

        private List<EnemyLogic> _botList = new List<EnemyLogic>();

        #endregion
        

        #region Methods

        private void RemoveBot(EnemyLogic logic)
        {
            _botList.Remove(logic);
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
            EnemyBody[] bodies = GameObject.FindObjectsOfType<EnemyBody>();
            foreach (EnemyBody body in bodies)
            {
                EnemyLogic logic = new EnemyLogic(body);
                _botList.Add(logic);
                logic.OnDestroyEventHandler += RemoveBot;
            }
        }

        #endregion
    }
}
