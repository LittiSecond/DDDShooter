using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class GameOverLogick : BaseController, IInitialization
    {
        #region Fields

        private TimeRemaining _executeLogickTimer;
        private TimeRemaining _spawnPlayerTimer;

        private readonly float _afterGameOverDelay = 0.01f;
        private readonly float _respawnPlayerDelay = 3.0f;

        #endregion


        #region ClassLifeCycles



        #endregion


        #region Methods

        private void StartGameOverLogick()
        {
            _executeLogickTimer.AddTimeRemaining();
        }

        private void ExecuteLogick()
        {
            _spawnPlayerTimer.AddTimeRemaining();
        }

        private void SpawnPlayer()
        {
            ServiceLocator.Resolve<PlayerController>().SpawnPlayerCharacter();
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            PlayerManager.OnPlayerDeletedHandler += StartGameOverLogick;
            _executeLogickTimer = new TimeRemaining(ExecuteLogick, _afterGameOverDelay);
            _spawnPlayerTimer = new TimeRemaining(SpawnPlayer, _respawnPlayerDelay);
        }

        #endregion

    }
}
