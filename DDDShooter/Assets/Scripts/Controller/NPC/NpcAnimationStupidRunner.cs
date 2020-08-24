using System;
using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class NpcAnimationStupidRunner
    {
        #region Fields

        private Animator _animator;

        private float _maxSpeed = 1.0f;

        #endregion


        #region ClassLifeCycles

        public NpcAnimationStupidRunner(Animator animator, NpcSettings settings)
        {
            _animator = animator;
            if (settings != null)
            {
                _maxSpeed = settings.PatrolSpeed;
            }
        }

        #endregion



    }
}
