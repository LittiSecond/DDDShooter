using System;
using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class EnemyAnimation
    {
        #region Fields

        private Animator _animator;

        private NpcAnimationState _state;
        private NpcAnimationState _beforeStopState;

        private readonly float _noMoveSpeed = 0.1f;

        private readonly int _walkAnimatorParameter = Animator.StringToHash("walk");
        private readonly int _walkRifleAimAnimatorParameter = Animator.StringToHash("walkRifleAim");
        private readonly int _deadAimatorParameter = Animator.StringToHash("dead");

        #endregion


        #region ClassLifeCycles

        public EnemyAnimation(Animator animator)
        {
            _animator = animator;
        }

        #endregion


        #region Methods

        public void ChangeState(NpcAnimationState newState)
        {
            switch (newState)
            {
                case NpcAnimationState.Inspection:
                    _animator.SetBool(_walkAnimatorParameter, false);
                    _animator.SetBool(_walkRifleAimAnimatorParameter, false);
                    break;
                case NpcAnimationState.Walk:
                    _animator.SetBool(_walkAnimatorParameter, true);
                    _animator.SetBool(_walkRifleAimAnimatorParameter, false);
                    break;
                case NpcAnimationState.WalkAim:
                    _animator.SetBool(_walkAnimatorParameter, false);
                    _animator.SetBool(_walkRifleAimAnimatorParameter, true);
                    break;
                case NpcAnimationState.Died:
                    //_animator.enabled = false;
                    _animator.SetBool(_deadAimatorParameter, true);
                    break;
                default:
                    CustumDebug.Log("EnemyAnimation->ChangeState: newState = " + newState.ToString());
                    break;
            }
            _state = newState;
            _beforeStopState = newState;
        }

        public void ChangeSpeed(float newSpeed)
        {
            switch (_state)
            {
                case NpcAnimationState.Inspection:
                    if (_beforeStopState == NpcAnimationState.Walk)
                    {
                        if (newSpeed > _noMoveSpeed)
                        {
                            ChangeState(NpcAnimationState.Walk);
                            //  _beforeStopState = NpcAnimationState.Inspection;
                        }
                    }
                    break;
                case NpcAnimationState.Walk:
                    if (newSpeed < _noMoveSpeed)
                    {
                        ChangeState(NpcAnimationState.Inspection);
                        _beforeStopState = NpcAnimationState.Walk;
                    }

                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
