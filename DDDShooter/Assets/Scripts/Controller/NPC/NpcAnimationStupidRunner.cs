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


        private readonly int _deadAimatorParameter = Animator.StringToHash("dead");
        private readonly int _blendAimatorParameter = Animator.StringToHash("Blend");

        #endregion


        #region ClassLifeCycles

        public NpcAnimationStupidRunner(Animator animator, NpcSettings settings)
        {
            _animator = animator;
            if (settings != null)
            {
                _maxSpeed = settings.Speed;
            }
        }

        #endregion


        #region Methods

        public void ChangeState(NpcAnimationState newState)
        {
            switch (newState)
            {
                case NpcAnimationState.Died:
                    _animator.SetBool(_deadAimatorParameter, true);
                    break;
                default:
                    _animator.SetBool(_deadAimatorParameter, false);
                    break;
            }
        }

        public void ChangeSpeed(float newSpeed)
        {
            float blend = newSpeed / _maxSpeed;
            _animator.SetFloat(_blendAimatorParameter, blend);
        }

        #endregion
    }
}
